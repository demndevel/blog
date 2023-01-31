using Application.Features.Notes.Queries.GetNotesByPage;

namespace Web.Models;

public class BlogViewModel
{
    public IList<GetNotesByPageQueryResultItem> Notes { get; set; } = null!;
}