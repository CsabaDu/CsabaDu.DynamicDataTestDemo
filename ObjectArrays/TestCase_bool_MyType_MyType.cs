namespace CsabaDu.DynamicDataTestDemo.ObjectArrays;

public record TestCase_bool_MyType_MyType(string TestCase, bool IsTrue, MyType MyType, MyType Other) : TestCase_bool_MyType(TestCase, IsTrue, MyType)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, MyType, Other];
}
