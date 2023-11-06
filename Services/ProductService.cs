using System.Text;
using ProductsApp.Exceptions;
using ProductsApp.Models.DTOs;
using ProductsApp.Services.Contracts;

namespace ProductsApp.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    
    private const string BaseUrl = "products/";
    
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<ProductDto>?> GetProductsAsync(int? minPrice = null, int? maxPrice = null)
    {
        var query = new StringBuilder();
        
        if (minPrice.HasValue || maxPrice.HasValue)
        {
            query.Append('?');
        }
        
        if (minPrice.HasValue)
        {
            query.Append($"minPrice={minPrice.Value}");
        }

        if (maxPrice.HasValue)
        {
            if (query.Length > 0)
            {
                query.Append('&');
            }
            query.Append($"maxPrice={maxPrice.Value}");
        }
        
        var response = await _httpClient.GetAsync(BaseUrl + query);
        
        if (!response.IsSuccessStatusCode)
            await GenerateApiException(response);
        
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
    }

    public async Task<ProductDto?> GetProductAsync(string id)
    {
        var response = await _httpClient.GetAsync(BaseUrl + id);
        
        if (!response.IsSuccessStatusCode)
            await GenerateApiException(response);
        
        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }

    public async Task<IEnumerable<ProductReportDto>?> GetProductReportAsync()
    {
        var response = await _httpClient.GetAsync(BaseUrl + "report");
        
        if (!response.IsSuccessStatusCode)
            await GenerateApiException(response);
        
        return await response.Content.ReadFromJsonAsync<IEnumerable<ProductReportDto>>();
    }

    public async Task<ProductDto?> CreateProductAsync(ProductUpsertDto product)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, product);
        
        if (!response.IsSuccessStatusCode)
            await GenerateApiException(response);
        
        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }

    public async Task UpdateProductAsync(string id, ProductUpsertDto product)
    {
        var response = await _httpClient.PutAsJsonAsync(BaseUrl + id, product);
        
        if (!response.IsSuccessStatusCode)
            await GenerateApiException(response);
    }

    public async Task DeleteProductAsync(string id)
    {
        var response = _httpClient.DeleteAsync(BaseUrl + id);
        
        if (!response.Result.IsSuccessStatusCode)
            await GenerateApiException(response.Result);
    }

    private static async Task GenerateApiException(HttpResponseMessage response)
    {
        var message = await response.Content.ReadAsStringAsync();
        var statusCode = response.StatusCode;
        throw new ApiException(message, statusCode);
    }
}