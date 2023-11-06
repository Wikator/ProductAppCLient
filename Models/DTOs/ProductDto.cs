namespace ProductsApp.Models.DTOs;

public class ProductDto
{
    public required string _id { get; set; }
    public required string Name { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public string? Description { get; set; }
}