using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class GetSaleResultTestData
    {
        private static readonly Faker<SaleItemResult> CreateSaleItemHandlerFaker = new Faker<SaleItemResult>()
            .RuleFor(i => i.Product, f => f.Commerce.ProductName())
            .RuleFor(i => i.Quantity, f => f.Random.Number(1, 10))
            .RuleFor(i => i.UnitPrice, f => decimal.Parse(f.Commerce.Price(1.0m, 100.0m)))
            .RuleFor(i => i.Discount, f => decimal.Parse(f.Commerce.Price(2.0m, 100.0m)));

        private static readonly Faker<GetSaleResult> GetSaleResultFaker = new Faker<GetSaleResult>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.SaleNumber, f => f.Random.Number(11, 99))
            .RuleFor(u => u.Branch, f => $"Filial{f.Random.Number(0, 10)}")
            .RuleFor(u => u.Customer, f => f.Person.FullName)
            .RuleFor(u => u.SaleDate, DateTime.Now)
            .RuleFor(u => u.TotalAmount, f => decimal.Parse(f.Commerce.Price(2.0m, 100.0m)))
            .RuleFor(u => u.IsCancelled, false)
            .RuleFor(u => u.SaleItem, f => CreateSaleItemHandlerFaker.Generate(f.Random.Number(1, 5)));

        public static GetSaleResult GenerateValidResult()
        {
            return GetSaleResultFaker.Generate();
        }
    }
}
