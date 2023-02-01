using Domain.Exceptions;

namespace Domain.Entities.Tag;

public class Text
{
    public string Value { get; }
    
    public Text(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > 20)
            throw new ValidationException($"Tag text {value} is invalid");
        Value = value;
    }
}