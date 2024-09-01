namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        var orderNameForGetOrders = OrderName.Of(request.Name);

        var ordersByName = dbContext.Orders.Include(order => order.OrderItems).AsNoTracking().Order()
            .Where(order => order.OrderName == orderNameForGetOrders);

        var ordersByNameDto = ordersByName.ToOrderDto();

        return new GetOrdersByNameResult(ordersByNameDto);
    }
}