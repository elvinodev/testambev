using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(i => i.Id, f => f.Random.Guid())
        .RuleFor(i => i.Product, f => f.Commerce.ProductName())
        .RuleFor(i => i.Quantity, f => f.Random.Number(1, 10))
        .RuleFor(i => i.UnitPrice, f => decimal.Parse(f.Commerce.Price(1.0m, 100.0m)));

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(u => u.Id, f => f.Random.Guid())
        .RuleFor(u => u.SaleNumber, f => f.Random.Number(11, 99))
        .RuleFor(u => u.Branch, f => $"Filial{f.Random.Number(0, 10)}")
        .RuleFor(u => u.Customer, f => f.Person.FullName)
        .RuleFor(u => u.SaleItem, f => SaleItemFaker.Generate(f.Random.Number(1, 5)));


    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }
}

