namespace Application.Features.Notes.Queries.GetNotesByTags;

public class GetNotesByTagsQueryResult
{
    public List<GetNotesByTagsQueryResultItem> Notes { get; set; } = new List<GetNotesByTagsQueryResultItem>();
}