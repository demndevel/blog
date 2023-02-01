namespace Application.Features.Notes.Commands.CreateNote;

public class CreateNoteCommand
{
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string Tags { get; set; } = "";
}