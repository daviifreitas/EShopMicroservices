namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product Product)
{
    public static implicit operator GetProductByIdResponse(GetProductByIdResult resultEntity)
    {
        return new GetProductByIdResponse(resultEntity.Product);
    }
}

public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                var apiResult = result;

                return Results.Ok(apiResult);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product by id endpoint");

    }
}
