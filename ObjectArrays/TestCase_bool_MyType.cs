namespace CsabaDu.DynamicDataTestDemo.ObjectArrays;

public record TestCase_bool_MyType(string TestCase, bool IsTrue, MyType MyType) : ObjectArray(TestCase)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, MyType];
}
