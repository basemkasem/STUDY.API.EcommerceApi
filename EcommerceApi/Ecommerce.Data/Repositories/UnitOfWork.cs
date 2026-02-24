using Ecommerce.Core.Interfaces.Repositories;

namespace Ecommerce.Data.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public IProductRepository Products => new ProductRepository(context);
    public ICategoryRepository Categories => new CategoryRepository(context);
    public ISaleRepository Sales => new SaleRepository(context);
    public async Task<int> CompleteAsync()
    {
       return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}