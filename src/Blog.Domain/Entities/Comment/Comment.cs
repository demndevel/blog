namespace Domain.Entities.Comment;

public class Comment
{
    public Guid Id { get; set; }
    public long PostId { get; set; }
    public string Name { get; set; } = null!;
    public string Text { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public DateTime DateCreated { get; set; }
}