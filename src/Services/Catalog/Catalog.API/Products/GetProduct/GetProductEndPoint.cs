namespace Catalog.API.Products.GetProduct;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetProductResponse(IEnumerable<Product> Products)
{
    public static implicit operator GetProductResponse(GetProductResult result)
    {
        return new GetProductResponse(result.Products);
    }
};

public class GetProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = new GetProductsQuery(request.PageNumber, request.PageSize);
                GetProductResult getProductResult = await sender.Send(query);

                GetProductResponse response = getProductResult;

                return Results.Ok(response);
            })
        .WithName("GetProducts")
        .Produces<GetProductResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get products")
        .WithDescription("For get all products created");
    }
}
