namespace Application.Features.Tags.Queries.GetAllTags;

public class GetAllTagsQueryResult
{
    public IList<GetAllTagsQueryResultItem> Tags { get; set; } = null!;
}

public class GetAllTagsQueryResultItem
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
}