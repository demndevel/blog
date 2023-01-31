namespace Web.Models;

public class EditNoteModel
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string Tags { get; set; } = null!;
}