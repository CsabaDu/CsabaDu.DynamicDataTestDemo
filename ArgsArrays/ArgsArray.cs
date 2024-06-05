namespace CsabaDu.DynamicDataTestDemo.ArgsArrays;

public abstract record ArgsArray(string TestCase)
{
    public virtual object[] ToObjectArray() => [TestCase];
}
