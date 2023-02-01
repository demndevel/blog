using Application.Features.Notes.Queries.GetNotesByTags;

namespace Web.Models;

public class SearchNotesByTagsViewModel
{
    public List<GetNotesByTagsQueryResultItem> Notes { get; set; } = new();
    public List<string> IncludedTags { get; set; } = new();
    public List<string> ExcludedTags { get; set; } = new();
}