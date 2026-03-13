using Ecommerce.Core.DTOs.Sale;
using Ecommerce.Core.Interfaces.Services;
using Ecommerce.Core.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController(ISaleService saleService) : ControllerBase
{
    private readonly ISaleService _saleService = saleService;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
    {
        var sales = await _saleService.GetAllSalesAsync(paginationParams);
        return Ok(sales.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var sale = await _saleService.GetSaleAsync(id);
        return sale.IsSuccess ? Ok(sale.Data) : NotFound(sale.Message);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSaleDto sale)
    {
        var result = await _saleService.CreateSaleAsync(sale);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
    }
}