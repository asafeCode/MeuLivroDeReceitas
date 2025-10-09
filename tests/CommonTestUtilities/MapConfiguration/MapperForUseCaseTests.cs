using CommonTestUtilities.Cryptography;

namespace CommonTestUtilities.MapConfiguration;

public abstract class MapperForUseCaseTests
{
    protected MapperForUseCaseTests()
    {
        MyRecipeBook.Application.Services.Mapper.MapConfigurations.Configure(IdRecipeEncripterBuilder.Build());
    }
}