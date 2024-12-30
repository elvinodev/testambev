using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class DeleteUserHandlerTestData
    {
        private static readonly Faker<DeleteUserCommand> DeleteUserHandlerFaker = new Faker<DeleteUserCommand>()
            .CustomInstantiator(f => new DeleteUserCommand(f.Random.Guid()));

        public static DeleteUserCommand GenerateValidCommand()
        {
            return DeleteUserHandlerFaker.Generate();
        }
    }
}
