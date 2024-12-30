using Ambev.DeveloperEvaluation.Domain.Events;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Events
{
    public class SaleCancelEventHandlerTest
    {
        [Fact]
        public async Task Handle_Should_WriteCorrectMessage_ToConsole()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var notification = new SaleCancelEvent(saleId);
            var cancellationToken = CancellationToken.None;

            var eventHandler = new SaleCancelEventHandler();

            // Act
            await eventHandler.Handle(notification, cancellationToken);

            // Assert
            var expectedMessage = $"Sale cancel success! ID: {saleId}, Data: {DateTime.Now:yyyy-MM-dd}";
        }
    }
}
