using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions;

public class ProductNotFoundException(Guid Id) : NotFoundException("Product not found", Id);
