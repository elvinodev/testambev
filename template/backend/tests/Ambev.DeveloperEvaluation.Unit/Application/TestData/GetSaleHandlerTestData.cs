using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class GetSaleHandlerTestData
    {
        private static readonly Faker<GetSaleCommand> GetSaleHandlerFaker = new Faker<GetSaleCommand>()
            .CustomInstantiator(f => new GetSaleCommand(f.Random.Guid()));

        public static GetSaleCommand GenerateValidCommand()
        {
            return GetSaleHandlerFaker.Generate();
        }
    }
}
