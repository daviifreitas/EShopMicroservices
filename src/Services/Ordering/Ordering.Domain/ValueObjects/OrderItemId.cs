namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    private OrderItemId(Guid value) => Value = value;

    public Guid Value { get; }

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