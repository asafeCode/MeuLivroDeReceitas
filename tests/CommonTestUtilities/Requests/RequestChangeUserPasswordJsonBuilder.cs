using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests;

public static class RequestChangeUserPasswordJsonBuilder
{
    public static RequestChangeUserPasswordJson Build(int passwordLength = 10)
    {
        return new Faker<RequestChangeUserPasswordJson>()
            .RuleFor(request => request.NewPassword, (f) => f.Internet.Password(passwordLength));
    }
}