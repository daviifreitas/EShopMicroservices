using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

public record GetOrdersRequest(PaginationRequest PaginationRequest);

public record GetOrdersResponse(PaginatedResult<OrderDto> Result);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Orders", async (GetOrdersRequest request, ISender sender) =>
            {
                var getOrdersQuery = new GetOrdersQuery(request.PaginationRequest);
                var ordersResult = await sender.Send(getOrdersQuery);
                var getOrdersResponse = new GetOrdersResponse(ordersResult.result);
                return Results.Ok(getOrdersResponse);
            })
            .WithName("Get orders")
            .WithDescription("Get orders endpoint")
            .WithSummary("Get orders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}