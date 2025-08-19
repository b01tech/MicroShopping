using MicroShopping.ProductAPI.Infra.Context;
using MicroShopping.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroShopping.ProductAPI.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly MySQLDbContext _context;

    public ProductRepository(MySQLDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetById(long id)
    {
        return await _context.Products
                        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Product?> Create(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> Update(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == product.Id);

        if (existingProduct == null)
            return null;


        _context.Entry(existingProduct).CurrentValues.SetValues(product);
        await _context.SaveChangesAsync();

        return existingProduct;
    }
    public async Task<bool> Delete(long id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }
}
