using Sqids;

namespace CommonTestUtilities.Cryptography;

public static class IdRecipeEncripterBuilder
{
    public static SqidsEncoder<long> Build()
    {
        return new SqidsEncoder<long>(new SqidsOptions
        {
            MinLength = 3,
            Alphabet = "91IvzMuF6nhEPYeHr2lZRmDUd5GStqgopfOkiJjcs0Bw3aNQWKL74XCbTyV8xA"
        });
    }
}