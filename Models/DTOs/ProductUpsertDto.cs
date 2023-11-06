using System.ComponentModel.DataAnnotations;

namespace ProductsApp.Models.DTOs;

public class ProductUpsertDto
{
    [Required] public string Name { get; set; } = "";
    
    [Range(0, double.MaxValue, ErrorMessage = "Price can't be negative.")]
    public decimal Price { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Amount can't be negative.")]
    public int Amount { get; set; }
    
    public string? Description { get; set; }
}