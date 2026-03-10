using Ecommerce.Core.DTOs.Sale;
using Ecommerce.Core.Interfaces.Repositories;
using Ecommerce.Core.Interfaces.Services;
using Ecommerce.Core.Models;
using Ecommerce.Core.Utilities;

namespace Ecommerce.Services;

public class SaleService(IUnitOfWork unitOfWork) : ISaleService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<SaleDto>> CreateSaleAsync(CreateSaleDto sale)
    {
        if (sale.Items.Count == 0)
            return Result<SaleDto>.Fail("Sale must have at least one item");
        var productIds = sale.Items.Select(i => i.ProductId).ToList();
        var productsFromDb = await _unitOfWork.Products.FindAsync(p => productIds.Contains(p.Id));
        var saleItems = new List<SaleItem>();
        foreach (var saleItem in sale.Items)
        {
            var product = productsFromDb.FirstOrDefault(p => p.Id == saleItem.ProductId);
            if (product is null)
                return Result<SaleDto>.Fail($"Product with Id {saleItem.ProductId} was not found");
            if (product.Quantity < saleItem.Quantity)
                return Result<SaleDto>.Fail($"Insufficient stock for product {product.Name}");

            product.Quantity -= saleItem.Quantity;
            saleItems.Add(new SaleItem()
            {
                ProductId = product.Id,
                Product = product,
                Quantity = saleItem.Quantity,
                UnitPriceAtTimeOfSale = product.Price
            });
        }

        var saleEntity = new Sale()
        {
            CreationDate = DateTime.UtcNow,
            Items = saleItems,
            TotalPrice = saleItems.Sum(i => i.Quantity * i.UnitPriceAtTimeOfSale)
        };
        
        _unitOfWork.Sales.Add(saleEntity);

        var rowsAffected = await _unitOfWork.CompleteAsync();
        if (rowsAffected == 0)
        {
            return Result<SaleDto>.Fail("Failed to create sale");
        }

        var saleDto = new SaleDto()
        {
            CreationDate = saleEntity.CreationDate,
            TotalPrice = saleEntity.TotalPrice,
            Id = saleEntity.Id,
            Items = saleItems.Select(si => new SaleItemDto()
            {
                ProductId = si.ProductId,
                ProductName = si.Product?.Name,
                Quantity = si.Quantity,
                UnitPrice = si.UnitPriceAtTimeOfSale
            }).ToList()
        };
        return Result<SaleDto>.Success(saleDto);
    }

    public Task<Result<SaleDto>> GetSaleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<SaleDto>>> GetAllSalesAsync()
    {
        throw new NotImplementedException();
    }
}