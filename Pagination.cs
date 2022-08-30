namespace Blog;

public abstract class PagedResultBase
{
    public int CurrentPage { get; init; }
    public int PageSize { get; init; } 
    public int RowCount { get; init; }
 
    public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

    public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
}
 
public class PagedResult<T> : PagedResultBase where T : class
{
    public IList<T> Results { get; set; }
 
    public PagedResult()
    {
        Results = new List<T>();
    }
}

public static class Pagination
{
    public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, 
        int page, int pageSize) where T : class
    {
        var result = new PagedResult<T>
        {
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = query.Count()
        };

        var skip = (page - 1) * pageSize;     
        result.Results = query.Skip(skip).Take(pageSize).ToList();
 
        return result;
    }
}