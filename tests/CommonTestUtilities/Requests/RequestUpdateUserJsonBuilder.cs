using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests;

public static class RequestUpdateUserJsonBuilder
{
    public static RequestUpdateUserJson Build()
    {
        return new Faker<RequestUpdateUserJson>()
            .RuleFor(request => request.NewName, f => f.Person.FirstName)
            .RuleFor(request => request.NewEmail, f => f.Internet.Email());
    }
}