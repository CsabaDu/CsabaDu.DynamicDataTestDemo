namespace CsabaDu.DynamicDataTestDemo.ArgsArrays;

public record TestCase_bool_MyType(string TestCase, bool IsTrue, MyType MyType) : ArgsArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, MyType];
}
