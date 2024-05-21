namespace CsabaDu.DynamicDataTestDemo.ObjectArrays;

public record TestCase_bool_MyType_object(string TestCase, bool IsTrue, MyType MyType, object Obj) : TestCase_bool_MyType(TestCase, IsTrue, MyType)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue, MyType, Obj];
}
