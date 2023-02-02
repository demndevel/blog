namespace Application.Features.Comments.Queries.GetCommentByPostId;

public class GetCommentByPostIdQueryResultItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime DateCreated { get; set; }
    public bool IsAdmin { get; set; }
}