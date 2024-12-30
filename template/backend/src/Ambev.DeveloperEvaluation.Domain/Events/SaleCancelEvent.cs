using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelEvent : INotification
    {
        public Guid SaleId { get; }

        public SaleCancelEvent(Guid id)
        {
            SaleId = id;
        }
    }

    public class SaleCancelEventEventHandler : INotificationHandler<SaleCancelEvent>
    {
        public Task Handle(SaleCancelEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Sale removed success! ID: {notification.SaleId}, Data: {DateTime.Now}");

            // Exemplo: enviar notificação, atualizar kafka, etc.

            return Task.CompletedTask;
        }
    }
}
