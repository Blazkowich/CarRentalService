namespace CarRental.Auth.BLL.Models.Search;

public class AuthSearchResult<T>
    where T : class
{
    public List<T> Items { get; set; }

    public int ItemsTotalCount { get; set; }

    public int PageNumber { get; set; }

    public int PerPage { get; set; }
}

