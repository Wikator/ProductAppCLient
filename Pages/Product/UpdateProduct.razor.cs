using AutoMapper;
using Microsoft.AspNetCore.Components;
using ProductsApp.Exceptions;
using ProductsApp.Models.DTOs;
using ProductsApp.Services.Contracts;

namespace ProductsApp.Pages.Product;

public partial class UpdateProduct
{
    [Inject] public required IProductService ProductService { get; init; }
    [Inject] public required NavigationManager NavigationManager { get; init; }
    [Inject] public required IMapper Mapper { get; init; }
    
    [Parameter] public string? Id { get; set; }
    
    private ProductUpsertDto? Model { get; set; }
    
    private string? FetchErrorMessage { get; set; }
    private string? UpdateErrorMessage { get; set; }
    
    private bool Submitting { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (Id is null)
            {
                NavigationManager.NavigateTo("/products");
            }
            else
            {
                var response = await ProductService.GetProductAsync(Id);
                
                if (response is null)
                {
                    NavigationManager.NavigateTo("/products");
                }
                else
                {
                    Model = Mapper.Map<ProductUpsertDto>(response);
                }
            }
        }
        catch (ApiException ex)
        {
            FetchErrorMessage = ex.Message;
        }
    }

    private async Task HandleSubmit()
    {
        try
        {
            if (Id is null || Model is null)
            {
                NavigationManager.NavigateTo("/products");
            }
            else
            {
                Submitting = true;
                await ProductService.UpdateProductAsync(Id, Model);
                NavigationManager.NavigateTo($"/products"); 
            }
        }
        catch (ApiException ex)
        {
            Submitting = false;
            UpdateErrorMessage = $"Server responded with status code: {ex.StatusCode} and message: {ex.Message}";
        }
    }
}