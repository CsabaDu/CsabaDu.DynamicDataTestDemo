namespace CsabaDu.DynamicDataTestDemo.ObjectArrays;

public abstract record ObjectArray(string TestCase)
{
    public virtual object[] ToObjectArray() => [TestCase];
}
