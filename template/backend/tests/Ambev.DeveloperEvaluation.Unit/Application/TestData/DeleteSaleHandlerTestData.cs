using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class DeleteSaleHandlerTestData
    {
        private static readonly Faker<DeleteSaleCommand> DeleteSaleHandlerFaker = new Faker<DeleteSaleCommand>()
            .CustomInstantiator(f => new DeleteSaleCommand(f.Random.Guid()));

        public static DeleteSaleCommand GenerateValidCommand()
        {
            return DeleteSaleHandlerFaker.Generate();
        }
    }
}
