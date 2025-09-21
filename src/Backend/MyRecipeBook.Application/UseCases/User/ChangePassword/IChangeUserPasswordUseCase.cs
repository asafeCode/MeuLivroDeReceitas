using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.User.ChangePassword;

public interface IChangeUserPasswordUseCase
{
    public Task Execute(RequestChangeUserPasswordJson request);
}