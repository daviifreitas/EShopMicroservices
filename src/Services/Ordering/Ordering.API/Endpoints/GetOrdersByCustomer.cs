using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.API.Endpoints;


public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer  : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var getOrdersByCustomerQuery = new GetOrdersByCustomerQuery(customerId);
            var ordersByCustomerResult = await sender.Send(getOrdersByCustomerQuery);
            var getOrdersByCustomerResponse = new GetOrdersByCustomerResponse(ordersByCustomerResult.Orders);
            return Results.Ok(getOrdersByCustomerResponse);
        })
        .WithName("Get orders by customer")
        .WithDescription("Get orders by customer endpoint")
        .WithSummary("Get orders by customer")
        .Produces<List<OrderDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}