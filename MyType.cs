using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.DynamicDataTestDemo;

public class MyType(int quantity, string label) : IEqualityComparer<MyType>
{
    public int Quantity { get; set; } = quantity;
    public string Label { get; set; } = label;

    public override bool Equals(object obj)
    {
        return obj is MyType other
            && other.Quantity == Quantity
            && other.Label == Label;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Quantity, Label);
    }

    public bool Equals(MyType x, MyType y)
    {
        return x?.Quantity == y?.Quantity;
    }

    public int GetHashCode([DisallowNull] MyType myType)
    {
        return myType.Quantity.GetHashCode();
    }
}