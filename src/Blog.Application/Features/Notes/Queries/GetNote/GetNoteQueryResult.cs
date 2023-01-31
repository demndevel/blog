namespace Application.Features.Notes.Queries.GetNote;

public class GetNoteQueryResult
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Tags { get; set; } = "";
}