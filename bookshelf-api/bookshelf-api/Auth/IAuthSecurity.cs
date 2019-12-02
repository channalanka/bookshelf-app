namespace bookshelf_api.Auth
{
    public interface IAuthSecurity
    {
        string GenerateToken(Token token);
        Token VerifyToken(string token);
        string GenerateHash(string data);
    }
}
