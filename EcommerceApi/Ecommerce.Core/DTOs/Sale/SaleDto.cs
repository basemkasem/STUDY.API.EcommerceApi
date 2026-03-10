namespace Ecommerce.Core.DTOs.Sale;

public class SaleDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<SaleItemDto> Items { get; set; } = new();
}

public class SaleItemDto
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}