namespace Application.Errors;

public class NotFoundException : Exception
{
    public NotFoundException(string type, string identifier)
        : base(message: $"{type} {identifier} not found.")
    { }
}