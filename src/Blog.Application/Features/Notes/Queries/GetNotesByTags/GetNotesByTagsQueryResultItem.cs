namespace Application.Features.Notes.Queries.GetNotesByTags;

public class GetNotesByTagsQueryResultItem
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Tags { get; set; } = null!;
    public DateTime Date { get; set; }
}