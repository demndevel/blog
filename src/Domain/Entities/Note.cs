namespace Domain.Entities;

public class Note
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; }
    public string Tags { get; set; } = "";
}