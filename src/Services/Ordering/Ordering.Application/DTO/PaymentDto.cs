namespace Ordering.Application.DTO
{
    public record PaymentDto(
        string CardNumber,
        string CardHolderName,
        DateTime Expiration,
        string Cvv,
        string PaymentMethod);
}
