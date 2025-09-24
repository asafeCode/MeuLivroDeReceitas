using Bogus;

namespace CommonTestUtilities.Requests;

public static class RequestStringGenerator
{
    public static string Paragraphs(int minCharacters) => new Faker().Lorem.Sentence(minCharacters);
}