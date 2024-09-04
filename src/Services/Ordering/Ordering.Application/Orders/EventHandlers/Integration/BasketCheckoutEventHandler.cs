using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Orders.Command.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation($"Integration event handled: {context.Message.GetType().Name}");

        var command = MapToCreateOrderCommand(context.Message);

        await sender.Send(command);
    }


    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent basketCheckoutEvent)
    {

        var addressDto = new AddressDto(basketCheckoutEvent.FirstName, basketCheckoutEvent.LastName,
            basketCheckoutEvent.EmailAddress ?? String.Empty, basketCheckoutEvent.AddressLine, basketCheckoutEvent.City,
            basketCheckoutEvent.State, basketCheckoutEvent.Country, basketCheckoutEvent.ZipCode);
        var paymentDto = new PaymentDto(basketCheckoutEvent.CardNumber, basketCheckoutEvent.CardHolderName,
            basketCheckoutEvent.Expiration, basketCheckoutEvent.CVV, basketCheckoutEvent.PaymentMethod);

        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto()
        {
            Id = orderId,
            OrderName = basketCheckoutEvent.UserName,
            TotalPrice = basketCheckoutEvent.TotalPrice,
            ShippingAddress = addressDto,
            BillingAddress = addressDto,
            Payment = paymentDto,
            OrderStatus = OrderStatus.Pending,
            OrderItems = new List<OrderItemDto>()
        };

        return new CreateOrderCommand(orderDto);
    }
}