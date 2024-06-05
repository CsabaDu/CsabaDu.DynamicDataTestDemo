namespace CsabaDu.DynamicDataTestDemo;

#nullable disable

[TestClass]
public sealed class MyTypeTests
{
    #region General test preparation
    private const string TestLabel = nameof(TestLabel); // NagyBetûs
    private const string testLabel = nameof(testLabel); // kisBetûs
    private const int TestQuantity = 3;
    private const int DifferentQuantity = 4;

    private int _quantity;
    private string _label;
    private MyType _myType;

    private MyType GetMyType()
    {
        return new(_quantity, _label);
    }
    #endregion

    #region Members for dynamic data tests
    private static readonly MyTypeTests Instance = new();

    private static IEnumerable<object[]> EqualsMyTypeArgs => Instance.GetEqualsMyTypeArgs();
    private static IEnumerable<object[]> EqualsObjectArgs => Instance.GetEqualsObjectArgs();
    private static IEnumerable<object[]> GetHashCodeArgs => Instance.GetGetHashCodeArgs();

    private const string DisplayName = nameof(GetDisplayName);

    public static string GetDisplayName(MethodInfo testMethodInfo, object[] argsArray)
    {
        string testMethodName = testMethodInfo.Name;
        string testCase = (string)argsArray[0];

        return $"{testMethodName}: {testCase}";
    }

    private MyType InitMyType()
    {
        _quantity = TestQuantity;
        _label = TestLabel;

        return GetMyType();
    }
    #endregion

    #region Dynamic data sources
    private IEnumerable<object[]> GetGetHashCodeArgs()
    {
        _myType = InitMyType();

        string testCase = "Different Quantity, same Label => false";
        _quantity = DifferentQuantity;
        MyType other = GetMyType();
        bool expected = false;
        yield return argsToObjectArray();

        testCase = "Same Quantity, same Label => true";
        _quantity = TestQuantity;
        other = GetMyType();
        expected = true;
        yield return argsToObjectArray();

        testCase = "Same Quantity, different Label => false";
        _label = testLabel;
        other = GetMyType();
        expected = false;
        yield return argsToObjectArray();

        #region argsToObjectArray
        object[] argsToObjectArray()
        {
            TestCase_bool_MyType_MyType args = new(testCase, expected, _myType, other);

            return args.ToObjectArray();
        }
        #endregion
    }

    private IEnumerable<object[]> GetEqualsObjectArgs()
    {
        _myType = InitMyType();

        string testCase = "null => false";
        object obj = null;
        bool expected = false;
        yield return argsToObjectArray();

        testCase = "object => false";
        obj = new();
        yield return argsToObjectArray();

        testCase = "Same MyType => true";
        obj = GetMyType();
        expected = true;
        yield return argsToObjectArray();

        testCase = "Different MyType => false";
        _quantity = DifferentQuantity;
        _label = testLabel;
        obj = GetMyType();
        expected = false;
        yield return argsToObjectArray();

        #region argsToObjectArray
        object[] argsToObjectArray()
        {
            TestCase_bool_MyType_object args = new(testCase, expected, _myType, obj);

            return args.ToObjectArray();
        }
        #endregion
    }

    private IEnumerable<object[]> GetEqualsMyTypeArgs()
    {
        _myType = InitMyType();

        string testCase = "null => false";
        MyType other = null;
        bool expected = false;

        TestCase_bool_MyType_MyType args = new(testCase, expected, _myType, other);
        object[] argsArray = args.ToObjectArray();

        // + Same Quantity, different Label => false;
        // + Different Quantity, same Label => false;
        // + Same Quantity, same Label => true;
        return GetHashCodeArgs.Append(argsArray);
    }
    #endregion

    #region Test methods
    [TestMethod]
    [DynamicData(nameof(GetHashCodeArgs), DynamicDataSourceType.Property, DynamicDataDisplayName = DisplayName)]
    public void GetHashCode_returns_expected(string testCase, bool expected, MyType myType, MyType other)
    {
        // Arrange
        int hashCode1 = myType.GetHashCode();
        int hashCode2 = other.GetHashCode();

        // Act
        var actual = hashCode1 == hashCode2;

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DynamicData(nameof(EqualsMyTypeArgs), DynamicDataSourceType.Property, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_MyType_returns_expected(string testCase, bool expected, MyType myType, MyType other)
    {
        // Arrange
        // Act
        var actual = myType.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DynamicData(nameof(EqualsObjectArgs), DynamicDataSourceType.Property, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_object_returns_expected(string testCase, bool expected, MyType myType, object obj)
    {
        // Arrange
        // Act
        var actual = myType.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
}

#nullable enable