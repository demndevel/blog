namespace Web.Models;

public enum MessageType
{
    Success,
    Failure,
    Information
}

public class MessageViewModel
{
    public string Title { get; set; } = null!;
    public string? Text { get; set; }
    public MessageType Type { get; set; } = MessageType.Information;
}