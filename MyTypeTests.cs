namespace CsabaDu.DynamicDataTestDemo;

#nullable disable

[TestClass]
public sealed class MyTypeTests
{
    #region General test preparation
    private const string TestLabel = nameof(TestLabel);
    private const string testLabel = nameof(testLabel);
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

    [TestCleanup]
    public void CleanupMyTypeTests()
    {
        _myType = null;
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

    private IEnumerable<object[]> GetGetHashCodeMyTypeArgs()
    {
        _myType = InitMyType();

        string testCase = "Different Quantities, same Labels => false";
        _quantity = DifferentQuantity;
        MyType other = GetMyType();
        bool expected = false;
        yield return argsToObjectArray();

        testCase = "Different Quantities, different Labels => false";
        _label = testLabel;
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
        _myType = InitMyType();

        string testCase = "null => false";
        object other = null;
        bool expected = false;
        yield return argsToObjectArray();

        testCase = "object => false";
        other = new();
        yield return argsToObjectArray();

        testCase = "Same MyType => true";
        other = GetMyType();
        expected = true;
        yield return argsToObjectArray();

        testCase = "Different MyType => false";
        _quantity = DifferentQuantity;
        _label = testLabel;
        other = GetMyType();
        expected = false;
        yield return argsToObjectArray();

        #region argsToObjectArray
        object[] argsToObjectArray()
        {
            TestCase_bool_MyType_object args = new(testCase, expected, _myType, other);

            return args.ToObjectArray();
        }
        #endregion
    }

    private IEnumerable<object[]> GetEqualsMyTypeArgs()
    {
        string testCase = "null => false";
        _myType = InitMyType();
        MyType other = null;
        bool expected = false;

        IEnumerable<object[]> argsList = GetGetGashCodeArgs();

        TestCase_bool_MyType_MyType args = new(testCase, expected, _myType, other);
        object[] argsArray = args.ToObjectArray();

        return argsList.Append(argsArray);
    }

    private IEnumerable<object[]> GetEqualsMyTypeMyTypeArgs()
    {
        IEnumerable<object[]> argsList = GetGetHashCodeMyTypeArgs();

        string testCase = "null, null => true";
        MyType x = null;
        MyType y = null;
        bool expected = true;
        addArgsToList();

        testCase = "MyType, null => false";
        x = GetMyType();
        expected = false;
        addArgsToList();

        testCase = "null, MyType => false";
        x = null;
        y = GetMyType();
        expected = false;
        addArgsToList();

        return argsList;

        #region addArgsToList
        void addArgsToList()
        {
            TestCase_bool_MyType_MyType args = new(testCase, expected, x, y);
            object[] argsArray = args.ToObjectArray();

            argsList = argsList.Append(argsArray);
        }
        #endregion
    }
    #endregion
    #endregion

    #region Test methods
    #region int GetHashCode
    #region MyType.GetHashCode()
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