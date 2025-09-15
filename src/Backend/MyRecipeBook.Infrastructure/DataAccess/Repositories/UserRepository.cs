using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext;

    public UserRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);
   
    public async Task<bool> ExistsActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
    public async Task<User?> GetEmailAndPassword(string email, string password)
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user 
                => user.Email.Equals(email) && user.Password.Equals(password));
    }

    public async Task<bool> ExistsActiveUserWithIdentifier(Guid userId) => await _dbContext.Users.AnyAsync(user => user.UserId.Equals(userId) && user.Active);
    
    public async Task<User?> GetUserByIdentifier(Guid userId)
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user 
                => user.Id.Equals(userId));
    }
    
    public async Task<User> GetById(long id)
    {
        return await _dbContext
            .Users
            .FirstAsync(user => user.Id == id);
    }

    public void Update(User user) =>  _dbContext.Users.Update(user);
  
}
