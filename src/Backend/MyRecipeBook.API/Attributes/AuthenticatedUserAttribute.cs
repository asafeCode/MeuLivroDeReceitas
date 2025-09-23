using System.Reflection.Metadata.Ecma335;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.API.Filters;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter)) {}
} 