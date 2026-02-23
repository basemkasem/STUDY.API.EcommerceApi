namespace Ecommerce.Core.Models;

public class Sale
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal TotalPrice { get; set; }
    public ICollection<SaleItem>? Items { get; set; } = new List<SaleItem>();
} 