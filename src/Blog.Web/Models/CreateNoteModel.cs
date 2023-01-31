namespace Web.Models;

public class CreateNoteModel
{
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string Tags { get; set; } = "";
}