using Basket.API.Dtos;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                var command = new CheckoutBasketCommand(request.BasketCheckoutDto);
                var result = await sender.Send(command, CancellationToken.None);

                return Results.Ok(new CheckoutBasketResponse(result.IsSuccess));
            })
            .WithName("basket checkout endpoint")
            .WithDescription("Basket checkout")
            .WithSummary("Checkout basket")
            .Produces<CheckoutBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}