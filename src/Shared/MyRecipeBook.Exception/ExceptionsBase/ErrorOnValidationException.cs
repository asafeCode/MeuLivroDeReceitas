﻿using System.Net;

namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : MyRecipeBookException
{
    private readonly IList<string> _errorMessages;

    public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
    {
        _errorMessages = errorMessages;
    }

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;


    public override IList<string> GetErrorMessage() => _errorMessages;
}
