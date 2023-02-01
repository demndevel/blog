using Domain.Exceptions;

namespace Domain.Entities.Note;

public class Tags
{
    public string Value { get; }

    public Tags(string value)
    {
        if (string.IsNullOrEmpty(value) || HasForbiddenChars(value))
            throw new ValidationException($"Tags {value} is invalid");
        
        Value = value;
    }

    private bool HasForbiddenChars(string value)
        => value.Contains(' ') || value.Contains('\\');
}