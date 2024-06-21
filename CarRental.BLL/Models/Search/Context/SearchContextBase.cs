using CarRental.BLL.Models.Enum;

namespace CarRental.BLL.Models.Search.Context;

public class SearchContextBase
{
    // Paging
    public int PageNumber { get; set; } = 1;

    public int PerPage { get; set; } = 100;

    // Sorting
    public SortingOptions SortDirection { get; set; } // can be replaced by enum

    public string SortField { get; set; } = "Id";
}

