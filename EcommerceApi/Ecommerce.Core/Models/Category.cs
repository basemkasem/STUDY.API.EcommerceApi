using Ecommerce.Core.Interfaces.Common;

namespace Ecommerce.Core.Models;

public class Category : IBaseEntity, ISoftDeletable
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Product>? Products { get; set; }
    
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}