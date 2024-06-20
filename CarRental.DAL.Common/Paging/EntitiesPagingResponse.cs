namespace CarRental.DAL.Common.Paging;

public class EntitiesPagingResponse<T>
    where T : class
{
    public List<T> Items { get; set; }

    public int ItemsTotalCount { get; set; }

    public int PageNumber { get; set; }

    public int PerPage { get; set; }
}

