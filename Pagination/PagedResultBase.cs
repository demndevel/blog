namespace Blog.Pagination;

public abstract class PagedResultBase
{
    public int CurrentPage { get; init; }
    public int PageSize { get; init; } 
    public int RowCount { get; init; }
 
    public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

    public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
}