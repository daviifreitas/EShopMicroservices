namespace Catalog.API.Products.GetProductByCategory;

// public record GetProductByCategoryRequest(string Category);

public record GetProductByCategoryResponse(IEnumerable<Product> Products)
{
    public static implicit operator GetProductByCategoryResponse(GetProductByCategoryResult productsForReturn)
    {
        return new GetProductByCategoryResponse(productsForReturn.Products);
    }
}

public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/Category/{category}", async (string category, ISender sender) =>
            {
                var productCategoryRequest = new GetProductByCategoryQuery(category);

                var result = await sender.Send(productCategoryRequest);

                GetProductByCategoryResponse response = result;

                return Results.Ok(response);
            })
            .WithName("Get product by category")
            .Produces<GetProductByCategoryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get products by category")
            .WithDescription("Get products by category");
    }
}
