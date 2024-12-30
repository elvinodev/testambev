using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Events
{
    public class UserRegisteredEventHandlerTest
    {
        [Fact]
        public async Task Handle_Should_WriteCorrectMessage_ToConsole()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var notification = new UserRegisteredEvent(UserTestData.GenerateValidUser());
            var cancellationToken = CancellationToken.None;

            var eventHandler = new UserRegisteredEventHandler();

            // Act
            await eventHandler.Handle(notification, cancellationToken);

            // Assert
            var expectedMessage = $"User create success! ID: {userId}, Data: {DateTime.Now:yyyy-MM-dd}";
        }
    }
}
