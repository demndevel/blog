namespace Application.Features.Comments.Queries.GetUnreadComments;

public class GetUnreadCommentsResultItem
{
    public Guid Id { get; set; }
    public long PostId { get; set; }
    public string Name { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime DateCreated { get; set; }
}