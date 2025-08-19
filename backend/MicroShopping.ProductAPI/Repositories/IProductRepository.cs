using MicroShopping.ProductAPI.Models;

namespace MicroShopping.ProductAPI.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetById(long id);
    Task<Product?> Create(Product product);
    Task<Product?> Update(Product product);
    Task<bool> Delete(long id);
}
