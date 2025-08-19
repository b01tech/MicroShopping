using MicroShopping.ProductAPI.Models;

namespace MicroShopping.ProductAPI.DTOs;

public record ProductDTO(string Name, string Description, decimal Price, string Category, string ImageUrl)
{
    public Product ToProduct()
    {
        return new Product(
             name: Name,
             description: Description,
             price: Price,
             category: Category,
             imageUrl: ImageUrl);

    }
    public static ProductDTO FromProduct(Product product)
    {
        return new ProductDTO(
            Name: product.Name,
            Description: product.Description!,
            Price: product.Price,
            Category: product.Category,
            ImageUrl: product.ImageUrl);
    }
}
