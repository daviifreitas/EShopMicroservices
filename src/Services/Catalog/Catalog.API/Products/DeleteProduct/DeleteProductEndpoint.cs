
namespace Catalog.API.Products.DeleteProduct
{
    // public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                DeleteProductCommand command = new DeleteProductCommand(id);
                DeleteProductResult result = await sender.Send(command);
                DeleteProductResponse response = new DeleteProductResponse(result.IsSuccess);

                return Results.Ok(response);
            })
            .WithName("Delete product")
            .WithDescription("Delete product Endpoint")
            .Produces<DeleteProductResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
        }
    }
}