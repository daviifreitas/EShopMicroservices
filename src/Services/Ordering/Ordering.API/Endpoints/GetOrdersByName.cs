using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{name}", async (string name, ISender sender) =>
            {
                var getOrdersByNameQuery = new GetOrdersByNameQuery(name);
                var ordersByNameResult = await sender.Send(getOrdersByNameQuery);
                var getOrdersByNameResponse = new GetOrdersByNameResponse(ordersByNameResult.Orders);
                return Results.Ok(getOrdersByNameResponse);
            })
            .WithName("Get orders by name")
            .WithDescription("Get orders by name endpoint")
            .WithSummary("Get orders by name")
            .Produces<List<OrderDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}