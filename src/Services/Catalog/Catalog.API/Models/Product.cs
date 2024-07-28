using Marten.Schema;

namespace Catalog.API.Models;

[DocumentAlias("cat_project")]
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<string> Category { get; set; } = new();
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }

    public Product()
    {
        
    }
    public Product(string name, string description, List<string> categories, string imageFile, decimal price)
    {
        Name = name;
        Description = description;
        Category = categories;
        ImageFile = imageFile;
        Price = price;
        Id = new Guid( /* 00000000-0000-0000-0000-000000000000 */);
    }
}
