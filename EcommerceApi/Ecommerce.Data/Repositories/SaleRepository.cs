using Ecommerce.Core.Interfaces.Repositories;
using Ecommerce.Core.Models;

namespace Ecommerce.Data.Repositories;

public class SaleRepository(AppDbContext context) : GenericRepository<Sale>(context), ISaleRepository
{
}