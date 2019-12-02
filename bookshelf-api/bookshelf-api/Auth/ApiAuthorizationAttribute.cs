using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bookshelf_api.Auth
{
    public class ApiAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            try
            {
                var authHeader = context.HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authHeader))
                {
                    Console.WriteLine("----UN AUTHRIZATION FILTER NO TOKEN-------");
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var token = authHeader.ToString().Split(' ')[1];
                IAuthSecurity securityService = (IAuthSecurity)context.HttpContext.RequestServices.GetService(typeof(IAuthSecurity));
                var resolveToken = securityService.VerifyToken(token);
                if (resolveToken == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var claims = new List<Claim>();
                claims.Add(new Claim("userId", resolveToken.Payload.UserId.ToString()));
                claims.Add(new Claim("userName", resolveToken.Payload.UserName));
                var claimUser = new ClaimsPrincipal(new ClaimsIdentity(claims));
                context.HttpContext.User = claimUser;

                Console.WriteLine("----AUTHRIZATION FILTER CALLED - Sucess-------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("----AUTHRIZATION FILTER CALLED -Exception-------" + ex.Message);
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
