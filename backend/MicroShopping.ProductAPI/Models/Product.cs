namespace MicroShopping.ProductAPI.Models;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public string ImageUrl { get; private set; } = string.Empty;

    protected Product()    {   }

    public Product(string name, string description, decimal price, string category, string imageUrl)
    {
        Validate(name, description, price, category, imageUrl);

        Name = name;
        Description = description;
        Price = price;
        Category = category;
        ImageUrl = imageUrl;
    }

    private static void Validate(string name, string description, decimal price, string category, string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));
    }
}
