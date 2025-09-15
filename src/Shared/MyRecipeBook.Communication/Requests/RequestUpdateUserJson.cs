namespace MyRecipeBook.Communication.Requests;

public class RequestUpdateUserJson
{
    public string NewName { get; set; } = string.Empty;
    
    public string NewEmail { get; set; } =  string.Empty;
}