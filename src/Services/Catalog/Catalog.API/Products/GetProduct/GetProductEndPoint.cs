using OpenTelemetry.Trace;

namespace Catalog.API.Products.GetProduct;

// public record GetProductsRequest();

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
        app.MapGet("/products", async (ISender sender) =>
        {
            GetProductResult getProductResult = await sender.Send(new GetProductsQuery());

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
