namespace Application.Features.Comments.Queries.GetUnreadComments;

public class GetUnreadCommentsResult
{
    public List<GetUnreadCommentsResultItem> Comments { get; set; } = new();
}