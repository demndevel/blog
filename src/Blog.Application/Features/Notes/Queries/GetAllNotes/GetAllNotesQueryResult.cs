namespace Application.Features.Notes.Queries.GetAllNotes;

public class GetAllNotesQueryResult
{
    public List<GetAllNotesQueryResultItem> Notes { get; set; } = new();
}