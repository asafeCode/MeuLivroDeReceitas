namespace MyRecipeBook.Communication.Requests;

public class RequestUserRegisterJson
{
    public string? Name { get; set; } 
    public string? Email { get; set; }
    public string? Password { get; set; }
}
