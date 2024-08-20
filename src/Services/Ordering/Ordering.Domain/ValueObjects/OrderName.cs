namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private OrderName(string Value)
    {
        this.Value = Value;
    }

    public string Value { get; init; }
    private const int DefaultLength = 5;
    
    public static OrderName Of(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentOutOfRangeException.ThrowIfEqual(value.Length, DefaultLength);

        return new OrderName(value);
    }

}