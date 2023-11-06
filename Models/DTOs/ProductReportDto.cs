namespace ProductsApp.Models.DTOs;

public class ProductReportDto
{
    public required string Name { get; set; }
    public int TotalQuantity { get; set; }
    public double TotalValue { get; set; }
}