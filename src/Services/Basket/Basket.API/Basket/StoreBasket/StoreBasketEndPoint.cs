using Microsoft.AspNetCore.Http.HttpResults;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var result = await sender.Send(new StoreBasketCommand(request.Cart));
                var response = new StoreBasketResponse(result.userName);

                return Results.Ok(response);
            })
            .WithName("Store basket")
            .WithDescription("store basket and return username")
            .WithSummary("Store basket")
            .Produces<StoreBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
