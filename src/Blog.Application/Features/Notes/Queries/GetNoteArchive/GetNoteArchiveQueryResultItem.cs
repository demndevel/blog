namespace Application.Features.Notes.Queries.GetNoteArchive;

public class GetNoteArchiveQueryResultItem
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime Date { get; set; }
}