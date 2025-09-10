using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using UnauthorizedAccessException = MyRecipeBook.Exceptions.ExceptionsBase.UnauthorizedAccessException;

namespace MyRecipeBook.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator  _accessTokenValidator;
    private readonly IUserReadOnlyRepository _userRepository;

    public AuthenticatedUserFilter(IAccessTokenValidator  accessTokenValidator, IUserReadOnlyRepository userRepository)
    {
        _accessTokenValidator = accessTokenValidator;
        _userRepository =  userRepository;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);
            var userId = _accessTokenValidator.ValidateAndGetUserId(token);
            var exists = await _userRepository.ExistsActiveUserWithIdentifier(userId);
            if (exists.IsFalse())
            {
                throw new UnauthorizedAccessException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            }
        }
        catch (SecurityTokenException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenIsExpired")
            {
                TokenIsExpired = true
            });
        }
        catch (MyRecipeBookException exception)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(exception.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(authentication))
        {
            throw new UnauthorizedAccessException(ResourceMessagesException.NO_TOKEN);
        }
        return authentication["Bearer ".Length..].Trim();
    }

}