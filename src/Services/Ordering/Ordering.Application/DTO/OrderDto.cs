
namespace Ordering.Application.DTO;

public record OrderDto
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string OrderName { get; init; }
    public AddressDto ShippingAddress { get; init; }
    public AddressDto BillingAddress { get; init; }
    public PaymentDto Payment { get; init; }
    public OrderStatus OrderStatus { get; init; }
    public decimal TotalPrice { get; init; }
    public List<OrderItemDto> OrderItems { get; init; }
}