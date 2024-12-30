using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CancelSaleHandlerTestData
    {
        private static readonly Faker<CancelSaleCommand> CancelSaleHandlerFaker = new Faker<CancelSaleCommand>()
            .CustomInstantiator(f => new CancelSaleCommand(f.Random.Guid()));

        public static CancelSaleCommand GenerateValidCommand()
        {
            return CancelSaleHandlerFaker.Generate();
        }
    }
}
