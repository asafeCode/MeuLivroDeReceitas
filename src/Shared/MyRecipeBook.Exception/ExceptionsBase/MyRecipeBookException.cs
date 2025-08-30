using System.Net;

namespace MyRecipeBook.Exceptions.ExceptionsBase;

public abstract class MyRecipeBookException : SystemException
{
    protected MyRecipeBookException(string messages) : base(messages)
    {
        
    }

    public abstract HttpStatusCode GetStatusCode();
    public abstract IList<string> GetErrorMessage();

}
