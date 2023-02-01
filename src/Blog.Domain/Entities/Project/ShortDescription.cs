using Domain.Exceptions;

namespace Domain.Entities.Project;

public class ShortDescription
{
    public string Value { get; }
    
    public ShortDescription(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > 1000)
            throw new ValidationException($"Short description {value} is invalid");
        Value = value;
    }
}