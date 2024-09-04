namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);

public record CreateOrderResponse(Guid OrderId);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.Map("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var createOrderCommand = new CreateOrderCommand(request.Order);
                var createOrderResult = await sender.Send(createOrderCommand);
                return new CreateOrderResponse(createOrderResult.Id);
            })
            .WithDescription("Create order endpoint")
            .WithName("Create Order")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create order");
    }
}