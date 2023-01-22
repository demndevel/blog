namespace Web.Pagination;

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