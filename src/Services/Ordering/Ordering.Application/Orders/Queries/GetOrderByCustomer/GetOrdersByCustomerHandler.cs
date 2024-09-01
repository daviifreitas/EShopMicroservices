using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var customerId = CustomerId.Of(request.CustomerId);
        var ordersByCustomer = dbContext.Orders.Include(order => order.OrderItems).AsNoTracking()
            .Where(x => x.CustomerId == customerId);
        
        var ordersByCustomerDto = ordersByCustomer.ToOrderDto();
        
        return new GetOrdersByCustomerResult(ordersByCustomerDto);
    }
}