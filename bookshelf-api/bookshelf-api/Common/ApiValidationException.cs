using System;
namespace bookshelf_api.Common
{
    public class ApiValidationException : Exception
    {
        public int StatusCode { get; set; }

        public string Value { get; set; }

        public ApiValidationException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
            Value = message;
        }
    }
}
