namespace Ecommerce.Core.DTOs.Product;

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
}