using Application.Features.Notes.Queries.GetNote;

namespace Web.Models;

public class NoteViewModel
{
    public GetNoteQueryResult Note { get; set; } = null!;
    public string[] Tags { get; set; } = Array.Empty<string>();
}