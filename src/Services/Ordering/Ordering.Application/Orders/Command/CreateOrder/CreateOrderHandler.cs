namespace Ordering.Application.Orders.Command.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = CreateNewOrder(request.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddressForNewOrder = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

        var billingAddressForNewOrder = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

        var paymentForNewOrder = Payment.Of(orderDto.Payment.CardNumber, orderDto.Payment.CardHolderName, orderDto.Payment.Expiration,
            orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        var newOrder = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(orderDto.CustomerId), OrderName.Of(orderDto.OrderName),
            shippingAddressForNewOrder, billingAddressForNewOrder, paymentForNewOrder);

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
        }

        return newOrder;
    }
}