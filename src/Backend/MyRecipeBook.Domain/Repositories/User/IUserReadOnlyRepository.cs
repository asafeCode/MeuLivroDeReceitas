namespace MyRecipeBook.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserWithEmail(string email);
    
    public Task<Entities.User?> GetEmailAndPassword(string email, string password);
    
    public Task<bool> ExistsActiveUserWithIdentifier(Guid userId);
    
    public Task<Entities.User?> GetUserByIdentifier(Guid userId);
}
