namespace CsabaDu.DynamicDataTestDemo;

public sealed class MyType(int quantity, string label) : IEquatable<MyType>
{
    public int Quantity { get; init; } = quantity;
    public string Label { get; init; } = label;

    public bool Equals(MyType? other)
    {
        return other is not null
            && other.Quantity == Quantity
            && other.Label == Label;
    }

    public override bool Equals(object? obj)
    {
        return obj is MyType other
            && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Quantity, Label);
    }
}