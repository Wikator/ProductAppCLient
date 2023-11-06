using ProductsApp.Models.DTOs;

namespace ProductsApp.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>?> GetProductsAsync(int? minPrice = null, int? maxPrice = null);
    Task<ProductDto?> GetProductAsync(string id);
    Task<IEnumerable<ProductReportDto>?> GetProductReportAsync();
    Task<ProductDto?> CreateProductAsync(ProductUpsertDto product);
    Task UpdateProductAsync(string id, ProductUpsertDto product);
    Task DeleteProductAsync(string id);
}