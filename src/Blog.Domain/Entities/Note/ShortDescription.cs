using Domain.Exceptions;

namespace Domain.Entities.Note;

public class ShortDescription
{
    public string Value { get; }

    public ShortDescription(string value)
    {
        if (value.Length > 500 || string.IsNullOrEmpty(value))
            throw new ValidationException($"Short description {value} is invalid");
        
        Value = value;
    }
}