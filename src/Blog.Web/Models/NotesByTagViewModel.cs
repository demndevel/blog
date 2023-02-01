using Application.Features.Notes.Queries.GetNotesByTags;

namespace Web.Models;

public class NotesByTagViewModel
{
    public string Tag { get; set; } = null!;
    public List<GetNotesByTagsQueryResultItem> Notes { get; set; } = new();
}