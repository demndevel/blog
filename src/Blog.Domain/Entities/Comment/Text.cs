using Domain.Exceptions;

namespace Domain.Entities.Comment;

public class Text
{
    public string Value { get; }
    
    public Text(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > 1000)
            throw new ValidationException("Text must be between 1 and 1000 characters.");
        
        Value = value;
    }
}