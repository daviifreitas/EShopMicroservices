namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    private OrderItemId(Guid Id) => this.Id = Id;

    public Guid Id { get; init; }

    public static OrderItemId Of(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);
        if (id == Guid.Empty)
        {
            throw new DomainException("OrderItemId cannot be empty");
        }

        return new OrderItemId(id);
    }
}