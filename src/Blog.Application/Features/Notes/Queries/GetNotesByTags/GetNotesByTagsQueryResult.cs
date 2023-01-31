namespace Application.Features.Notes.Queries.GetNotesByTags;

public class GetNotesByTagsQueryResult
{
    public IList<GetNotesByTagsQueryResultItem> Notes { get; set; } = new List<GetNotesByTagsQueryResultItem>();
}