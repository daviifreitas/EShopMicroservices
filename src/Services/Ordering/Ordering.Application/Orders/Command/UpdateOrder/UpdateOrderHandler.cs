
namespace Ordering.Application.Orders.Command.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderIdForGetOrderForUpdate = OrderId.Of(request.Order.Id);
        Order? order = await dbContext.Orders.FindAsync(orderIdForGetOrderForUpdate);

        if (order == null)
        {
            throw new OrderNotFoundException(request.Order.Id);
        }

        UpdateOrderWithNewValuesFromCommand(request, order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    private static void UpdateOrderWithNewValuesFromCommand(UpdateOrderCommand request, Order order)
    {
        var shippingAddressForUpdateOrder = Address.Of(request.Order.ShippingAddress.FirstName,
            request.Order.ShippingAddress.LastName,
            request.Order.ShippingAddress.EmailAddress, request.Order.ShippingAddress.AddressLine,
            request.Order.ShippingAddress.Country, request.Order.ShippingAddress.State,
            request.Order.ShippingAddress.ZipCode);

        var billingAddressForUpdate = Address.Of(request.Order.BillingAddress.FirstName,
            request.Order.BillingAddress.LastName,
            request.Order.BillingAddress.EmailAddress, request.Order.BillingAddress.AddressLine,
            request.Order.BillingAddress.Country,
            request.Order.BillingAddress.State, request.Order.BillingAddress.ZipCode);
        var paymentForUpdate = Payment.Of(request.Order.Payment.CardNumber, request.Order.Payment.CardHolderName,
            request.Order.Payment.Expiration,
            request.Order.Payment.Cvv, request.Order.Payment.PaymentMethod);

        order.Update(OrderName.Of(request.Order.OrderName), shippingAddressForUpdateOrder, billingAddressForUpdate,
            paymentForUpdate, request.Order.OrderStatus);
    }
}