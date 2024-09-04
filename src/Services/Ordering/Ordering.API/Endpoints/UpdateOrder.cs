using Ordering.Application.Orders.Command.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Oder);

public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var updateOrderCommand = new UpdateOrderCommand(request.Oder);
            var updateOrderResult = await sender.Send(updateOrderCommand);

            
            return Results.Ok(new UpdateOrderResponse(updateOrderResult.IsSuccess));
        })
        .WithName("Update order")
        .WithDescription("Update order endpoint")
        .WithSummary("Update order")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}