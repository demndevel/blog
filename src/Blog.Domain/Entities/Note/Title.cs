using Domain.Exceptions;

namespace Domain.Entities.Note;

public class Title
{
    public string Value { get; }

    public Title(string value)
    {
        if (value.Length > 100 || string.IsNullOrEmpty(value))
            throw new ValidationException($"Title {value} is invalid");

        Value = value;
    }
}