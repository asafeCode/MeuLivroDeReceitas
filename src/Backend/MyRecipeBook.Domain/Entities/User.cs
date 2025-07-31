namespace MyRecipeBook.Domain.Entities;

//teste de pull req na develop
//aprendendo git da forma correta
//feature mais
//teste de merge na develop atualizada por outro dev
public class User : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

