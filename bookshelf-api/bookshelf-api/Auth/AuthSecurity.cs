using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace bookshelf_api.Auth
{
    public class AuthSecurity : IAuthSecurity
    {
        private  string sharedKey;
        public AuthSecurity(IConfiguration config) {
            this.sharedKey = config["Security:SymetricKey"];
        }


        public string GenerateToken(Token token)
        {
            try
            {
                
                var data = JsonConvert.SerializeObject(token);
                var payloadBytes = Encoding.UTF8.GetBytes(data);
                
                var keyByte = Convert.FromBase64String(sharedKey);
                string signedString = "";
                using (var hmac = new HMACSHA256(keyByte))
                {
                    var signedByte = hmac.ComputeHash(payloadBytes);
                    signedString = Convert.ToBase64String(signedByte);
                }
                
                string payloadString = System.Convert.ToBase64String(payloadBytes);
                return string.Format("{0}.{1}", signedString, payloadString);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Token VerifyToken(string token)
        {
            try
            {
                var tokenData = token.Split('.');
                var payload = Convert.FromBase64String(tokenData[1]);
                var payloadStr = Encoding.UTF8.GetString(payload);
                var payloadByte = Encoding.UTF8.GetBytes(payloadStr);
                Console.Write(payloadByte);

                var keyByte = Convert.FromBase64String(sharedKey);
                byte[] signedByte = null;
                using (var hmac = new HMACSHA256(keyByte))
                {
                    signedByte = hmac.ComputeHash(payloadByte);

                }

                var signedToken = Convert.FromBase64String(tokenData[0]);
                Console.Write("TOKEN SIGNED TO RESOLVE");
                Console.Write(signedToken);
                if (!compareTwobyteArray(signedToken, signedByte))
                    return null;
                Console.Write("--------RESOLVED TOKEN----------");
                Console.Write(payloadStr);
                var tokenObj = JsonConvert.DeserializeObject<Token>(payloadStr);
                if (tokenObj.Expired < DateTime.Now)
                    return null;
                return tokenObj;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }


        bool compareTwobyteArray(byte[] a1, byte[] a2)
        {
            bool bEqual = false;
            if (a1.Length == a2.Length)
            {
                int i = 0;
                while ((i < a1.Length) && (a1[i] == a2[i]))
                {
                    i += 1;
                }
                if (i == a1.Length)
                {
                    bEqual = true;
                }
            }

            return bEqual;

        }

        public string GenerateHash(string data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
