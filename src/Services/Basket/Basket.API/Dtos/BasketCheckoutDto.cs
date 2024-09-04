namespace Basket.API.Dtos;

public class BasketCheckoutDto
{
    public string UserName { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;

    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string? EmailAddress { get; init; } = default!;
    public string AddressLine { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string State { get; init; } = default!;
    public string City { get; init; } = default!;
    public string ZipCode { get; init; } = default!;


    public string CardNumber { get; init; } = default!;
    public string CardHolderName { get; init; } = default!;
    public DateTime Expiration { get; init; }
    public string CVV { get; init; } = default!;
    public string PaymentMethod { get; init; } = default!;
}