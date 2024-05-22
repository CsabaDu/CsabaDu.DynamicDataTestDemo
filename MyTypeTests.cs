using System.Reflection;
using CsabaDu.DynamicDataTestDemo.ObjectArrays;

namespace CsabaDu.DynamicDataTestDemo;

[TestClass]
public sealed class MyTypeTests
{
    #region Test prep
    #region Fields
    #region Static fields
    private const string TestLabel = nameof(TestLabel);
    private const string DifferentLabel = nameof(DifferentLabel);
    private static readonly MyTypeTests Instance = new();
    #endregion Static fields

    private int _quantity;
    private string _label;
    private MyType _myType;
    #endregion Fields

    #region Static properties
    private static IEnumerable<object[]> EqualsMyTypeMyTypeArgs => Instance.GetEqualsMyTypeMyTypeArgs();
    private static IEnumerable<object[]> EqualsObjectArgs => Instance.GetEqualsObjectArgs();
    #endregion Static properties

    [TestInitialize]
    public void InitMyTypeTests()
    {
        _quantity = GetRandomQuantity();
        _label = TestLabel;
        _myType = GetMyType();
    }
    #endregion Test prep

    #region Test methods
    #region bool Equals
    #region MyType.Equals(object)
    [TestMethod]
    [DynamicData(nameof(EqualsObjectArgs), DynamicDataSourceType.Property, DynamicDataDisplayName = nameof(GetDisplayName))]
    public void Equals_arg_object_returns_expected(string testCase, bool expected, MyType myType, object obj)
    {
        // Arrange
        // Act
        var actual = myType.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion MyType.Equals(object)

    #region IEqualityComparer.Equals(MyType, MyType)
    [TestMethod]
    [DynamicData(nameof(EqualsMyTypeMyTypeArgs), DynamicDataSourceType.Property, DynamicDataDisplayName = nameof(GetDisplayName))]
    public void Equals_args_MyType_MyType_returns_expected(string testCase, bool expected, MyType x, MyType y)
    {
        // Arrange
        // Act
        var actual = _myType.Equals(x, y);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion IEqualityComparer.Equals(MyType, MyType)
    #endregion bool Equals

    #region int GetHashCode
    #region MyType.GetHashCode()
    [TestMethod]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        int expected = HashCode.Combine(_quantity, _label);

        // Act
        var actual = _myType.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion MyType.GetHashCode()

    #region IEqualityComparer.GetHashCode(MyType)
    [TestMethod]
    public void GetHashCode_arg_MyType_returns_expected()
    {
        // Arrange
        _quantity = GetRandomQuantity();
        MyType other = GetMyType();
        int expected = _quantity.GetHashCode();

        // Act
        var actual = _myType.GetHashCode(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion IEqualityComparer.GetHashCode(MyType)
    #endregion int GetHashCode
    #endregion Test methods

    #region Test helpers
    #region Dynamic data
    private IEnumerable<object[]> GetEqualsObjectArgs()
    {
        _myType = GetMyType();

        string testCase = "null => false";
        object obj = null;
        bool expected = false;
        yield return argsToObjectArray();

        testCase = "object => false";
        obj = new();
        yield return argsToObjectArray();

        testCase = "Same Quantity, same Label => true";
        obj = GetMyType();
        expected = true;
        yield return argsToObjectArray();

        testCase = "Different Quantity, same Label => false";
        _quantity = GetRandomQuantity(_quantity);
        obj = GetMyType();
        expected= false;
        yield return argsToObjectArray();

        testCase = "Different Quantity, different Label => false";
        _label = DifferentLabel;
        obj = GetMyType();
        yield return argsToObjectArray();

        testCase = "Same Quantity, different Label => false";
        _quantity = _myType.Quantity;
        obj = GetMyType();
        yield return argsToObjectArray();

        #region argsToObjectArray
        object[] argsToObjectArray()
        {
            TestCase_bool_MyType_object args = new(testCase, expected, _myType, obj);

            return args.ToObjectArray();
        }
        #endregion
    }

    private IEnumerable<object[]> GetEqualsMyTypeMyTypeArgs()
    {
        string testCase = "null, null => true";
        MyType x = null;
        MyType y = null;
        bool expected = true;
        yield return argsToObjectArray();

        testCase = "MyType, null => false";
        x = GetMyType();
        expected = false;
        yield return argsToObjectArray();

        testCase = "Same Quantities, same Labels => true";
        y = GetMyType();
        expected = true;
        yield return argsToObjectArray();

        testCase = "null, MyType => false";
        x = null;
        expected = false;
        yield return argsToObjectArray();

        testCase = "Different Quantities, same Labels => false";
        _quantity = GetRandomQuantity(_quantity);
        x = GetMyType();
        yield return argsToObjectArray();

        testCase = "Different Quantities, different Labels => false";
        _label = DifferentLabel;
        x = GetMyType();
        yield return argsToObjectArray();

        testCase = "Same Quantities, different Labels => true";
        _quantity = y.Quantity;
        x = GetMyType();
        expected = true;
        yield return argsToObjectArray();

        #region argsToObjectArray
        object[] argsToObjectArray()
        {
            TestCase_bool_MyType_MyType args = new(testCase, expected, x, y);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion Dynamic data

    private static int GetRandomQuantity(int? excluded = null)
    {
        Random random = new();
        int quantity = getRandomQuantity();

        if (excluded is null)
        {
            return quantity;
        }

        while (quantity == excluded.Value)
        {
            quantity = getRandomQuantity();
        }

        return quantity;

        #region Local methods
        int getRandomQuantity()
        {
            return random.Next(int.MinValue, int.MaxValue);
        }
        #endregion
    }

    private MyType GetMyType()
    {
        return new(_quantity, _label);
    }

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        string methodName = methodInfo.Name;
        string testCase = (string)args[0];

        return $"{methodName}: {testCase}";
    }
    #endregion
}