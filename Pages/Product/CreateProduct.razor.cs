using Microsoft.AspNetCore.Components;
using ProductsApp.Exceptions;
using ProductsApp.Models.DTOs;
using ProductsApp.Services.Contracts;

namespace ProductsApp.Pages.Product;

public partial class CreateProduct
{
    [Inject] public required IProductService ProductService { get; init; }
    [Inject] public required NavigationManager NavigationManager { get; init; }
    
    private ProductUpsertDto Model { get; } = new();

    private bool Submitting { get; set; }
    
    private string? ErrorMessage { get; set; }
    
    private async Task HandleSubmit()
    {
        try
        {
            Submitting = true;
            await ProductService.CreateProductAsync(Model);
            NavigationManager.NavigateTo($"/products");
        }
        catch (ApiException ex)
        {
            Submitting = false;
            ErrorMessage = $"Server responded with status code: {ex.StatusCode} and message: {ex.Message}";
        }
    }
}