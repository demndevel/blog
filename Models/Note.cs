namespace Blog.Models;

public class Note
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public List<int> Tags { get; set; } = new();
}