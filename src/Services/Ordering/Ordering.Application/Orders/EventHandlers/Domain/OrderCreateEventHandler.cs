using MassTransit;
using MediatR;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreateEventHandler(
    IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager,
    ILogger<OrderCreateEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain event handled: {DomainEvent}", domainEvent.GetType().Name);
        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            var orderDto = domainEvent.Order.ToOrderDto();
            await publishEndpoint.Publish(orderDto, cancellationToken);   
        }
        
    }
}