namespace CsabaDu.DynamicDataTestDemo;

public sealed class MyType(int quantity, string label) : IEqualityComparer<MyType>
{
    public int Quantity { get; init; } = quantity;
    public string Label { get; init; } = label;

    /// <summary>
    /// Egyenlőség összehasonlítás egy másik objektummal.
    /// </summary>
    /// <param name="obj">Összehasonlítandó objektum</param>
    /// <returns>Ha a paraméter MyType-példány és minden tulajdonsága egyenlő, akkor true, egyébként false.</returns>
    public override bool Equals(object obj)
    {
        return obj is MyType other
            && other.Quantity == Quantity
            && other.Label == Label;
    }

    /// <summary>
    /// Egyenlőség összehasonlítása két MyType-példány null-értének, vagy Quantity-tulajdonságuk értékének.
    /// </summary>
    /// <param name="x">Egyik MyType-példány vagy null</param>
    /// <param name="y">Másik MyType-példány vagy null</param>
    /// <returns>Ha mindkét paraméter null, vagy a mindkettőnek a Quantity-tulajdonsága egyenlő, akkor true, egyébként false.</returns>
    public bool Equals(MyType x, MyType y)
    {
        return x?.Quantity == y?.Quantity;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Quantity, Label);
    }

    public int GetHashCode([DisallowNull] MyType myType)
    {
        return myType.Quantity.GetHashCode();
    }
}