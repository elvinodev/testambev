using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class GetUserResultTestData
    {
        private static readonly Faker<SaleItemResult> CreateSaleItemHandlerFaker = new Faker<SaleItemResult>()
            .RuleFor(i => i.Product, f => f.Commerce.ProductName())
            .RuleFor(i => i.Quantity, f => f.Random.Number(1, 10))
            .RuleFor(i => i.UnitPrice, f => decimal.Parse(f.Commerce.Price(1.0m, 100.0m)))
            .RuleFor(i => i.Discount, f => decimal.Parse(f.Commerce.Price(2.0m, 100.0m)));

        private static readonly Faker<GetUserResult> GetUserResultFaker = new Faker<GetUserResult>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Name, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
            .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
            .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin));

        public static GetUserResult GenerateValidResult()
        {
            return GetUserResultFaker.Generate();
        }
    }
}
