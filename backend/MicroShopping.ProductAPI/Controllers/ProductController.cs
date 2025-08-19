using MicroShopping.ProductAPI.DTOs;
using MicroShopping.ProductAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MicroShopping.ProductAPI.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        var productDtos = products.Select(ProductDTO.FromProduct).ToList();
        return Ok(productDtos);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(ProductDTO.FromProduct(product));
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDto)
    {
        if (productDto == null)
        {
            return BadRequest("Product cannot be null");
        }
        var product = productDto.ToProduct();
        var createdProduct = await _productRepository.Create(product);

        return Created("", ProductDTO.FromProduct(createdProduct!));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(long id, [FromBody] ProductDTO productDto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        existingProduct.Update(
            name: productDto.Name,
            description: productDto.Description,
            price: productDto.Price,
            category: productDto.Category,
            imageUrl: productDto.ImageUrl
        );
        var updatedProduct = await _productRepository.Update(existingProduct);
        return Ok(ProductDTO.FromProduct(updatedProduct));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        var isDeleted = await _productRepository.Delete(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
