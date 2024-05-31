namespace CsabaDu.DynamicDataTestDemo;

public sealed class MyType(int quantity, string label) : IEquatable<MyType>
{
    public int Quantity { get; init; } = quantity;
    public string Label { get; init; } = label;

    /// <summary>
    /// Beágyazott osztály az IEqualityComparer<MyType>.Equals-metódus implementációhoz
    /// </summary>
    public sealed class QuantityEqualityComparer : IEqualityComparer<MyType>
    {
        /// <summary>
        /// Egyenlőség összehasonlítása két MyType-példány null-értének, vagy Quantity-tulajdonságuk értékének.
        /// </summary>
        /// <returns>Ha mindkét paraméter null, vagy a mindkettőnek a Quantity-tulajdonsága egyenlő, akkor true, egyébként false.</returns>
        public bool Equals(MyType? x, MyType? y)
        {
            return x?.Quantity == y?.Quantity;
        }

        public int GetHashCode([DisallowNull] MyType myType)
        {
            return myType.Quantity.GetHashCode();
        }
    }

    /// <summary>
    /// Egyenlőség összehasonlítás egy másik MyType-példánnyal.
    /// </summary>
    /// <returns>Ha a paraméter MyType-példány és minden tulajdonsága egyenlő, akkor true, egyébként false.</returns>
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