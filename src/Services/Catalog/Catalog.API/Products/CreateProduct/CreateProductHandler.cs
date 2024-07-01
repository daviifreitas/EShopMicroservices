using Marten;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description,string ImageFile, decimal Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async  Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product productForCreateForCommand = new Product(request.Name, request.Description, request.Category,
            request.ImageFile, request.Price);
        
        session.Store(productForCreateForCommand);
        await session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(productForCreateForCommand.Id);
    }
}
