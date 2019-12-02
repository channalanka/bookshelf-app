using System;
using System.IO;
using System.Threading.Tasks;
using bookshelf_api.Auth;
using bookshelf_api.Common;
using bookshelf_api.Models;
using bookshelf_api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;

namespace bookshelf_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));
            services.AddScoped<IAuthSecurity, AuthSecurity>();
            services.AddScoped<IUser, UserRepo>();
            services.AddScoped<BookshelfContext>();
            services.AddScoped<IBookShelf, BookShelfRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseCors();
            //intrgate token endpoint handle custom middleware
            app.Use(authMiddleware);
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        /// <summary>
        /// token end point implementation
        /// </summary>

        private Func<HttpContext, Func<Task>, Task> authMiddleware = async (context, next) => {
            Console.WriteLine("REQUEST PATH -  " + context.Request.Path);
            Console.WriteLine(context.Request);


            if (context.Request.Path == "/token")
            {
                JObject loginReq = null;
                // userservice.GetUserFromUserNameAndPassword(context.Request.Body.ReadAsync())
                using (var reader = new StreamReader(context.Request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    loginReq = JObject.Parse(body);
                    Console.WriteLine(body);

                    // Do something
                }
                var userName = loginReq.GetValue("username", StringComparison.OrdinalIgnoreCase)?.Value<string>();
                var password = loginReq.GetValue("password", StringComparison.OrdinalIgnoreCase)?.Value<string>();

                if (loginReq == null || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid username or Password");
                }
                else
                {

                    IUser userRepo = context.RequestServices.GetService<IUser>();
                    var user = await userRepo.GetUserByUserName(userName, password);
                    if (user == null)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Invalid username or Password");
                    }

                    IAuthSecurity securityService = context.RequestServices.GetService<IAuthSecurity>();
                    var tokenData = new Token(new Payload
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        Name = user.Name
                    }, DateTime.Now.AddDays(1));
                    var token = securityService.GenerateToken(tokenData);
                    await context.Response.WriteAsync(token);

                }

                return;
            }


            await next.Invoke();
        };
    }
}
