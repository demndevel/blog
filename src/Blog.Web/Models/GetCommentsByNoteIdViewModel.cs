using Application.Features.Comments.Queries.GetCommentByPostId;

namespace Web.Models;

public class GetCommentsByNoteIdViewModel
{
    public List<GetCommentByPostIdQueryResultItem> Comments { get; set; } = new();
    public long PostId { get; set; }
}