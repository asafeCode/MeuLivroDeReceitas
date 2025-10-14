using System.Net;

namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class NotFoundException : MyRecipeBookException
{
    public NotFoundException(string message) : base(message){}
    
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;

    public override IList<string> GetErrorMessage() => [Message];
}