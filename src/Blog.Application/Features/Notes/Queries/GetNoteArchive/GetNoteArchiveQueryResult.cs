namespace Application.Features.Notes.Queries.GetNoteArchive;

public class GetNoteArchiveQueryResult
{
    public List<GetNoteArchiveQueryResultItem> Notes { get; set; } = new();
}