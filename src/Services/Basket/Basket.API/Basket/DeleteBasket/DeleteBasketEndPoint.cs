using Microsoft.AspNetCore.Http.HttpResults;

namespace Basket.API.Basket.DeleteBasket;


public record DeleteBasketResponse(bool IsDeleted);

public class DeleteBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                DeleteBasketResult deleteBasketResult = await sender.Send(new DeleteBasketCommand(userName));

                var response = new DeleteBasketResponse(deleteBasketResult.IsDeleted);

                return Results.Ok(response);
            })
            .WithName("Delete basket")
            .WithDescription("Delete basket by userName")
            .WithSummary("Delete basket by userName")
            .Produces<DeleteBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
