﻿using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestUserRegisterJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}