using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class UserRegisteredEvent : INotification
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }

    public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"User create success! ID: {notification.User.Id}, Data: {DateTime.Now}");

            // Exemplo: enviar notificação, atualizar kafka, etc.

            return Task.CompletedTask;
        }
    }
}
