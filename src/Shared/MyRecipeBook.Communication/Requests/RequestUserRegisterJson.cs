using System.ComponentModel.DataAnnotations;

namespace MyRecipeBook.Communication.Requests;

public class RequestUserRegisterJson
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
