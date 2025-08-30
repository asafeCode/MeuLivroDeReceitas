using Bogus;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace CommonTestUtilities.Requests;

public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {
        return new Faker<RequestLoginJson>()
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email())
            .RuleFor(user => user.Password, (f, user) => f.Internet.Password());
    }
}