using Marten;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryResult> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {Request}", request);

            var productsFromCategory = await session.Query<Product>()
                .Where(product => product.Category.Contains(request.Category))
                .ToListAsync(token: cancellationToken);

            return new GetProductByCategoryResult(productsFromCategory);
        }
    }
}
