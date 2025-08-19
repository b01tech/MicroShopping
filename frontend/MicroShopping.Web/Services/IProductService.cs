using MicroShopping.Web.Models;

namespace MicroShopping.Web.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(long id);
    Task<Product?> Create(Product product);
    Task<Product?> Update(Product product);
    Task<bool> Delete(long id);
}
