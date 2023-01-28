namespace Application.Helpers;

public class Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
{
    private static readonly Unit _value = new();
    
    public static ref readonly Unit Value => ref _value;

    public static Task<Unit> Task { get; } = System.Threading.Tasks.Task.FromResult(_value);


    public bool Equals(Unit? other) => true;

    public int CompareTo(Unit? other) => 0;

    public int CompareTo(object? obj) => 0;

    public override int GetHashCode() => 0;

    public override bool Equals(object? obj) => obj is Unit;
    
    public static bool operator ==(Unit first, Unit second) => true;
    
    public static bool operator !=(Unit first, Unit second) => false;
    
    public override string ToString() => "()";
}