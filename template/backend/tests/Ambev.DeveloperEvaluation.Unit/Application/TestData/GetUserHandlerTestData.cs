using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class GetUserHandlerTestData
    {
        private static readonly Faker<GetUserCommand> GetUserHandlerFaker = new Faker<GetUserCommand>()
            .CustomInstantiator(f => new GetUserCommand(f.Random.Guid()));

        public static GetUserCommand GenerateValidCommand()
        {
            return GetUserHandlerFaker.Generate();
        }
    }
}
