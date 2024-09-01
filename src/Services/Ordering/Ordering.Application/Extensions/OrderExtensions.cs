namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDto(this IEnumerable<Order> orders)
    {
        var ordersByNameDto = orders.Select(order => new OrderDto
        {
            Id = order.Id.Value,
            OrderName = order.OrderName.Value,
            Payment = new PaymentDto(order.Payment.CardNumber, order.Payment.CardHolderName, order.Payment.Expiration,
                order.Payment.CVV, order.Payment.PaymentMethod),
            BillingAddress = new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName,
                order.ShippingAddress.EmailAddress ?? "", order.ShippingAddress.AddressLine, order.ShippingAddress.City,
                order.ShippingAddress.State, order.ShippingAddress.Country, order.ShippingAddress.ZipCode),
            CustomerId = order.CustomerId.Value,
            OrderItems = order.OrderItems.Select(x => new OrderItemDto()
            {
                ProductId = x.ProductId.Value,
                Price = x.Price,
                Quantity = x.Quantity,
                OrderId = x.OrderId.Value,
            }).ToList(),
            OrderStatus = order.OrderStatus,
            ShippingAddress = new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName,
                order.ShippingAddress.EmailAddress ?? "", order.ShippingAddress.AddressLine, order.ShippingAddress.City,
                order.ShippingAddress.State, order.ShippingAddress.Country, order.ShippingAddress.ZipCode),
            TotalPrice = order.TotalPrice
        });
        return ordersByNameDto;
    }
}