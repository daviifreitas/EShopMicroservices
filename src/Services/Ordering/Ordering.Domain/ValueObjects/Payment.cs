namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardNumber { get; init; } = default!;
    public string CardHolderName { get; init; } = default!;
    public DateTime Expiration { get; init; }
    public string CVV { get; init; } = default!;
    public string PaymentMethod { get; init; } = default!;
    
    protected Payment()
    {
        
    }
    
    private Payment(string cardNumber, string cardHolderName, DateTime expiration, string cvv, string paymentMethod)
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }
    
    public static Payment Of(string cardNumber, string cardHolderName, DateTime expiration, string cvv, string paymentMethod)
    {
        ArgumentNullException.ThrowIfNull(cardNumber);
        ArgumentNullException.ThrowIfNull(cardHolderName);
        ArgumentNullException.ThrowIfNull(cvv);
        ArgumentOutOfRangeException.ThrowIfEqual(cvv.Length, 3);
        
        return new Payment(cardNumber, cardHolderName, expiration, cvv, paymentMethod);
    }
}