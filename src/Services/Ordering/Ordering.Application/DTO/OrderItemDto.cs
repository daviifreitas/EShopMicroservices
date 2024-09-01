namespace Ordering.Application.DTO;

public record OrderItemDto
{
    public Guid OrderId { get; init; }
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}