using Ecommerce.Core.Interfaces.Common;

namespace Ecommerce.Core.Models;

public class Sale : IBaseEntity, ISoftDeletable
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal TotalPrice { get; set; }
    public ICollection<SaleItem>? Items { get; set; } = new List<SaleItem>();

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}