using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Events
{
    public class SaleRegisteredEventHandlerTest
    {
        [Fact]
        public async Task Handle_Should_WriteCorrectMessage_ToConsole()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var notification = new SaleRegisteredEvent(SaleTestData.GenerateValidSale());
            var cancellationToken = CancellationToken.None;

            var eventHandler = new SaleRegisteredEventHandler();

            // Act
            await eventHandler.Handle(notification, cancellationToken);

            // Assert
            var expectedMessage = $"Sale create success! ID: {saleId}, Data: {DateTime.Now:yyyy-MM-dd}";
        }
    }
}
