using Domain.Exceptions;

namespace Domain.Entities.Project;

public class Title
{
    public string Value { get; }
    
    public Title(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > 100)
            throw new ValidationException($"Title {value} is invalid");
        Value = value;
    }
}