namespace Application.Features.Notes.Queries.GetNotesByTags;

public class GetNotesByTagsQuery
{
    public string IncludedTags { get; set; } = "";
    public string ExcludedTags { get; set; } = "";
}