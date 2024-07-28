
namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string IamgeFile, decimal Price)
    {
        public static implicit operator UpdateProductCommand(UpdateProductRequest request)
        {
            return new UpdateProductCommand(request.Id, request.Name, request.Category, request.Description, request.IamgeFile, request.Price);
        }
    }
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                UpdateProductCommand command = request;
                UpdateProductResult result = await sender.Send(command);
                UpdateProductResponse response = new UpdateProductResponse(result.IsSuccess);

                return Results.Ok(response);
            })
            .WithName("Update Product endpoint")
            .WithDescription("Endpoint for update product")
            .Produces<UpdateProductResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
