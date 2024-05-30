#nullable disable

namespace CsabaDu.DynamicDataTestDemo;

[TestClass]
public sealed class MyTypeTests
{
    #region General test preparation
    private const string TestLabel = nameof(TestLabel);
    private const string DifferentLabel = nameof(DifferentLabel);
    private const int TestQuantity = 3;
    private const int DifferentQuantity = 4;
    private readonly MyType.QuantityEqualityComparer _comparer = new();

    private int _quantity;
    private string _label;
    private MyType _myType;

    [TestInitialize]
    public void InitMyTypeTests()
    {
        _quantity = TestQuantity;
        _label = TestLabel;
    }

    private MyType GetMyType()
    {
        return new(_quantity, _label);
    }
    #endregion

    #region Members for dynamic data tests
    private const string DisplayName = nameof(GetDisplayName);
    private static readonly MyTypeTests Instance = new();

    private static IEnumerable<object[]> EqualsMyTypeArgs => Instance.GetEqualsMyTypeArgs();

    private static IEnumerable<object[]> EqualsObjectArgs => Instance.GetEqualsObjectArgs();
    private static IEnumerable<object[]> EqualsMyTypeMyTypeArgs => Instance.GetEqualsMyTypeMyTypeArgs();
    private static IEnumerable<object[]> GetHashCodeArgs => Instance.GetGetGashCodeArgs();
    private static IEnumerable<object[]> GetHashCodeMyTypeArgs => Instance.GetGetHashCodeMyTypeArgs();

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        string methodName = methodInfo.Name;
        string testCase = (string)args[0];

        return $"{methodName}: {testCase}";
    }

    private MyType InitMyType()
    {
        _quantity = TestQuantity;
        _label = TestLabel;

        return GetMyType();
    }
    #endregion

    #region Dynamic data sources
    #region GetHashCode
    private IEnumerable<object[]> GetGetGashCodeArgs()
    {
        _myType = InitMyType();

        string testCase = "Same Quantity, same Label => true";
        object obj = GetMyType();
        bool expected = true;
        yield return argsToObjectArray();

        testCase = "Different Quantity, same Label => false";
        _quantity = DifferentQuantity;
        obj = GetMyType();
        expected = false;
        yield return argsToObjectArray();

        testCase = "Different Quantity, different Label => false";
        _label = DifferentLabel;
        obj = GetMyType();
        yield return argsToObjectArray();

        testCase = "Same Quantity, different Label => false";
        _quantity = TestQuantity;
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

    private IEnumerable<object[]> GetGetHashCodeMyTypeArgs()
    {
        _myType = InitMyType();

        string testCase = "Same Quantities, same Labels => true";
        MyType other = GetMyType();
        bool expected = true;
        yield return argsToObjectArray();

        testCase = "Different Quantities, same Labels => false";
        _quantity = DifferentQuantity;
        other = GetMyType();
        expected = false;
        yield return argsToObjectArray();

        testCase = "Different Quantities, different Labels => false";
        _label = DifferentLabel;
        other = GetMyType();
        yield return argsToObjectArray();

        testCase = "Same Quantities, different Labels => true";
        _quantity = TestQuantity;
        other = GetMyType();
        expected = true;
        yield return argsToObjectArray();

        #region argsToObjectArray
        object[] argsToObjectArray()
        {
            TestCase_bool_MyType_MyType args = new(testCase, expected, _myType, other);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion

    #region Equals
    private IEnumerable<object[]> GetEqualsObjectArgs()
    {
        IEnumerable<object[]> argsList = GetGetGashCodeArgs();
        _myType = InitMyType();

        string testCase = "null => false";
        object obj = null;
        bool expected = false;
        argsListAddArgs();

        testCase = "object => false";
        obj = new();
        argsListAddArgs();

        return argsList;

        #region argsListAddArgs
        void argsListAddArgs()
        {
            TestCase_bool_MyType_object args = new(testCase, expected, _myType, obj);

            argsList = argsList.Append(args.ToObjectArray());
        }
        #endregion
    }

    private IEnumerable<object[]> GetEqualsMyTypeArgs()
    {
        string testCase = "null => false";
        _myType = InitMyType();
        MyType other = null;
        bool expected = false;
        yield return argsToObjectArray();

        foreach (object[] item in GetGetGashCodeArgs())
        {
            testCase = (string)item[0];
            expected = (bool)item[1];
            _myType = (MyType)item[2];
            other = (MyType)item[3];

            yield return argsToObjectArray();
        }

        #region argsToObjectArray
        object[] argsToObjectArray()
        {
            TestCase_bool_MyType_MyType args = new(testCase, expected, _myType, other);

            return args.ToObjectArray();
        }
        #endregion
    }

    private IEnumerable<object[]> GetEqualsMyTypeMyTypeArgs()
    {
        IEnumerable<object[]> argsList = GetGetHashCodeMyTypeArgs();

        string testCase = "null, null => true";
        MyType x = null;
        MyType y = null;
        bool expected = true;
        argsListAddArgs();

        testCase = "MyType, null => false";
        x = GetMyType();
        expected = false;
        argsListAddArgs();

        testCase = "null, MyType => false";
        x = null;
        y = GetMyType();
        expected = false;
        argsListAddArgs();

        return argsList;

        #region argsListAddArgs
        void argsListAddArgs()
        {
            TestCase_bool_MyType_MyType args = new(testCase, expected, x, y);

            argsList = argsList.Append(args.ToObjectArray());
        }
        #endregion
    }
    #endregion
    #endregion

    #region Test methods
    #region int GetHashCode
    #region MyType.GetHashCode()
    [TestMethod]
    public void GetHashCode_returns_constancy()
    {
        // Arrange
        _myType = GetMyType();

        // Act
        int hashCode1 = _myType.GetHashCode();
        int hashCode2 = _myType.GetHashCode();

        // Assert
        Assert.AreEqual(hashCode1, hashCode1);
    }

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
    #endregion

    #region IEqualityComparer.GetHashCode(MyType)
    [TestMethod]
    public void GetHashCode_arg_MyType_returns_constancy()
    {
        // Arrange
        _myType = GetMyType();

        // Act
        int hashCode1 = _comparer.GetHashCode(_myType);
        int hashCode2 = _comparer.GetHashCode(_myType);

        // Assert
        Assert.AreEqual(hashCode1, hashCode2);
    }

    [TestMethod]
    [DynamicData(nameof(GetHashCodeMyTypeArgs), DynamicDataSourceType.Property, DynamicDataDisplayName = DisplayName)]
    public void GetHashCode_arg_MyType_returns_expected(string testCase, bool expected, MyType x, MyType y)
    {
        // Arrange
        int hashCode1 = _comparer.GetHashCode(x);
        int hashCode2 = _comparer.GetHashCode(y);

        // Act
        bool actual = hashCode1 == hashCode2;

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool Equals
    #region IEquatable<MyType>.Equals(MyType?)
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
    #endregion

    #region MyType.Equals(object?)
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

    #region IEqualityComparer.Equals(MyType?, MyType?)
    [TestMethod]
    [DynamicData(nameof(EqualsMyTypeMyTypeArgs), DynamicDataSourceType.Property, DynamicDataDisplayName = DisplayName)]
    public void Equals_args_MyType_MyType_returns_expected(string testCase, bool expected, MyType x, MyType y)
    {
        // Arrange
        // Act
        var actual = _comparer.Equals(x, y);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion
    #endregion
}

#nullable enable