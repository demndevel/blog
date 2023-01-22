namespace Domain.Entities;

public class Project
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public string? Link { get; set; }
}