using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleItemCommand> CreateSaleItemHandlerFaker = new Faker<CreateSaleItemCommand>()
        .RuleFor(i => i.Product, f => f.Commerce.ProductName())
        .RuleFor(i => i.Quantity, f => f.Random.Number(1, 10))
        .RuleFor(i => i.UnitPrice, f => decimal.Parse(f.Commerce.Price(1.0m, 100.0m)));

    private static readonly Faker<CreateSaleCommand> CreateSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(u => u.Id, f => f.Random.Guid())
        .RuleFor(u => u.SaleNumber, f => f.Random.Number(11, 99))
        .RuleFor(u => u.Branch, f => $"Filial{f.Random.Number(0, 10)}")
        .RuleFor(u => u.Customer, f => f.Person.FullName)
        .RuleFor(u => u.SaleItem, f => CreateSaleItemHandlerFaker.Generate(f.Random.Number(1, 5)));


    public static CreateSaleCommand GenerateValidCommand()
    {
        return CreateSaleHandlerFaker.Generate();
    }
}
