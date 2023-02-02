using Domain.Exceptions;

namespace Domain.Entities.Comment;

public class Name
{
    public string Value { get; }
    
    public Name(string value)
    {
        if (value.Length > 50)
            throw new ValidationException("Name cannot be longer than 50 characters.");
        
        Value = string.IsNullOrWhiteSpace(value) ? "Anonymous" : value;
    }
}