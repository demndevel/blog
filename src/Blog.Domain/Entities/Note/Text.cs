using Domain.Exceptions;

namespace Domain.Entities.Note;

public class Text
{
    public string Value { get; }

    public Text(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ValidationException($"Text {value} is invalid");
        
        Value = value;
    }
}