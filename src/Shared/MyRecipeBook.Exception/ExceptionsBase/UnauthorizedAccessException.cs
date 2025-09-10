using System.Net;

namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class UnauthorizedAccessException : MyRecipeBookException
{
    public UnauthorizedAccessException(string message) : base(message){}
    
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    
    public override IList<string> GetErrorMessage() => [Message];
}