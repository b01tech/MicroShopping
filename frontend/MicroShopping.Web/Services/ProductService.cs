using MicroShopping.Web.Models;
using MicroShopping.Web.Utils;

namespace MicroShopping.Web.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    public const string BasePath = "/api/v1/product";

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync(BasePath);
        return await response.ReadContentAs<IEnumerable<Product>>();
    }

    public Task<Product?> GetByIdAsync(long id)
    {
        var response =  _httpClient.GetAsync($"{BasePath}/{id}");
        return response.Result.ReadContentAs<Product?>();
    }
    public async Task<Product?> Create(Product product)
    {
        var response = await _httpClient.PostAsJsonAsync(BasePath, product);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<Product?>();
        else
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<Product?> Update(Product product)
    {
        var response =  await _httpClient.PutAsJsonAsync(BasePath, product);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<Product?>();
        else
            throw new Exception("Something went wrong when calling API");
    }
    public async Task<bool> Delete(long id)
    {
        var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else
            throw new Exception("Something went wrong when calling API");
    }
}
