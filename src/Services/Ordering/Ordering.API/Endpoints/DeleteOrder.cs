using Ordering.Application.Orders.Command.DeleteOrder;
using Ordering.Domain.ValueObjects;

namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
            {
                var deleteOrderResult = await sender.Send(new DeleteOrderCommand(Id));

                return Results.Ok(new DeleteOrderResponse(deleteOrderResult.IsSuccess));
            }).WithName("Delete order")
            .WithDescription("Delete order endpoint")
            .WithSummary("Delete order application")
            .Produces<DeleteOrderResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}