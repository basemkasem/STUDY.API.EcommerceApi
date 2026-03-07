namespace Ecommerce.Core.Utilities;

public class PaginationParams
{
    private const int DefaultPageSize = 50;
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get;
        init => 
            field = value > DefaultPageSize ? DefaultPageSize : value;
    } = 10;
}