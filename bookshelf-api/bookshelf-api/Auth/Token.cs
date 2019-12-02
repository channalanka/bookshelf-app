using System;
namespace bookshelf_api.Auth
{
    public class Token
    {
        public Token(Payload payload, DateTime expired)
        {
            this.Payload = payload;
            this.Expired = expired;
        }
        public Payload Payload { get; set; }
        public DateTime Expired { get; set; }
    }
}
