namespace Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommand
{
    public long PostId { get; set; }
    public string Name { get; set; } = null!;
    public string Text { get; set; } = null!;
    public bool IsAdmin { get; set; }
}