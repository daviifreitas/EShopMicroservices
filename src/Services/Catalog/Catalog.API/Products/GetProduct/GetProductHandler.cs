using Marten;

namespace Catalog.API.Products.GetProduct;

public record GetProductsQuery() : IQuery<GetProductResult>;

public record GetProductResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@}", request);

        IEnumerable<Product> products = await session.Query<Product>().ToListAsync(token: cancellationToken);

        return new GetProductResult(products);
    }
}
