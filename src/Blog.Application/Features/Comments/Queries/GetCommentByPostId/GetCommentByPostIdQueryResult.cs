namespace Application.Features.Comments.Queries.GetCommentByPostId;

public class GetCommentByPostIdQueryResult
{
    public List<GetCommentByPostIdQueryResultItem> Comments { get; set; } = new();
}