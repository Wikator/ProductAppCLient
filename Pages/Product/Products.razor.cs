using Microsoft.AspNetCore.Components;
using ProductsApp.Exceptions;
using ProductsApp.Models.DTOs;
using ProductsApp.Services.Contracts;

namespace ProductsApp.Pages.Product;

public partial class Products
{
    [Inject] public required IProductService ProductService { get; init; }
    [Inject] public required NavigationManager NavigationManager { get; init; }
    
    private string? ProductListErrorMessage { get; set; }
    private List<string?> ProductDeleteErrorMessages { get; } = new();
    private string? ProductReportErrorMessage { get; set; }
    
    private IEnumerable<ProductDto>? ProductList { get; set; }
    private IEnumerable<ProductReportDto>? ProductReportList { get; set; }
    
    private int? MinPrice { get; set; }
    private int? MaxPrice { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ProductList = await ProductService.GetProductsAsync();
        }
        catch (ApiException ex)
        {
            ProductListErrorMessage = $"Server responded with status code: {ex.StatusCode} and message: {ex.Message}";
        }
    }

    private async Task FilterProducts()
    {
        try
        {
            ProductList = await ProductService.GetProductsAsync(MinPrice, MaxPrice);
        }
        catch (ApiException ex)
        {
            ProductListErrorMessage = $"Server responded with status code: {ex.StatusCode} and message: {ex.Message}";
        }
    }

    private async Task GenerateReport()
    {
        try
        {
            ProductReportList = await ProductService.GetProductReportAsync();
        }
        catch (ApiException ex)
        {
            ProductReportErrorMessage = $"Server responded with status code: {ex.StatusCode} and message: {ex.Message}";
        }
    }
    
    private async Task DeleteProduct(string id)
    {
        try
        {
            await ProductService.DeleteProductAsync(id);
            // ProductList = ProductList?.Where(p => p._id != id);
            NavigationManager.NavigateTo("/products", true);
        }
        catch (ApiException ex)
        {
            ProductDeleteErrorMessages.Add($"Server responded with status code: {ex.StatusCode} and message: {ex.Message}");
        }
    }
}