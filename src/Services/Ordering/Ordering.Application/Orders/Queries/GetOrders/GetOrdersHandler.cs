using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = dbContext.Orders.Include(x => x.OrderItems).AsNoTracking().OrderBy(x => x.OrderName.Value)
            .Skip(request.paginationRequest.PageIndex * request.paginationRequest.PageIndex)
            .Take(request.paginationRequest.PageSize).AsEnumerable().ToOrderDto();
        
        var ordersQuantity = await dbContext.Orders.LongCountAsync(cancellationToken);

        var paginatedResult = new PaginatedResult<OrderDto>(request.paginationRequest.PageIndex,
            request.paginationRequest.PageSize, ordersQuantity, orders);

        return new GetOrdersResult(paginatedResult);
    }
}