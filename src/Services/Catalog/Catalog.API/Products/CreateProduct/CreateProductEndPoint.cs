﻿namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);

public record CreateProductResponse(Guid Id);

public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                CreateProductCommand productCreateCommandFromRequest = request.Adapt<CreateProductCommand>();
                CreateProductResult resultForProducts = await sender.Send(productCreateCommandFromRequest);
                CreateProductResponse response = resultForProducts.Adapt<CreateProductResponse>();

                return Results.Created($"/products/${response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .WithSummary("Create product")
            .WithDescription("http request for create product");
    }
}
