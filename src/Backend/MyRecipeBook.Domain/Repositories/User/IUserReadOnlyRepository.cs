namespace MyRecipeBook.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserWithEmail(string email);
    
    public Task<Entities.User?> GetEmailAndPassword(string name, string password);
}
