namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                GetBasketResult getBasketResult = await sender.Send(new GetBasketQuery(userName));

                var getBasketResponse = new GetBasketResponse(getBasketResult.Cart);

                return Results.Ok(getBasketResponse);
            })
            .WithName("Get basket by userName")
            .Produces<GetBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get basket by userName")
            .WithDescription("Get basket by userName");
    }
}
