namespace CsabaDu.DynamicDataTestDemo;

#nullable disable

[TestClass]
public sealed class MyTypeTests
{
    #region General test preparation
    private const string TestLabel = nameof(TestLabel); // UpperCase
    private const string testLabel = nameof(testLabel); // lowerCase
    private const int TestQuantity = 3;
    private const int DifferentQuantity = 4;

    private int _quantity;
    private string _label;
    private MyType _myType;

    private object _obj;
    private MyType _other;
    int _hashCode1;
    int _hashCode2;

    private MyType GetMyType()
    {
        return new(_quantity, _label);
    }

    private void InitMyType()
    {
        _quantity = TestQuantity;
        _label = TestLabel;

        _myType = GetMyType();
    }
    #endregion

    #region Simple tests
    #region Members for simple tests
    [TestInitialize]
    public void InitMyTypeTests()
    {
        InitMyType();
    }

    [TestCleanup]
    public void CleanupMyTypeTests()
    {
        _obj = default;
        _other = default;
        _label = default;
        _quantity = default;
    }
    #endregion

    #region Simple test methods
    #region Equals(object?)
    [TestMethod]
    public void Equals_object_null_returns_false()
    {
        // Arrange
        _obj = null;

        // Act
        var actual = _myType.Equals(_obj);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Equals_object_object_returns_false()
    {
        // Arrange
        _obj = new();

        // Act
        var actual = _myType.Equals(_obj);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Equals_object_differentMyType_returns_false()
    {
        // Arrange
        _quantity = DifferentQuantity;
        _label = testLabel;
        _obj = GetMyType();

        // Act
        var actual = _myType.Equals(_obj);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Equals_object_sameMyType_returns_true()
    {
        // Arrange
        _quantity = TestQuantity;
        _label = TestLabel;
        _obj = GetMyType();

        // Act
        var actual = _myType.Equals(_obj);

        // Assert
        Assert.IsTrue(actual);
    }
    #endregion

    #region Equals(MyType?)
    [TestMethod]
    public void Equals_MyType_null_returns_false()
    {
        // Arrange
        _other = null;

        // Act
        var actual = _myType.Equals(_other);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Equals_MyType_differentQuantity_returns_false()
    {
        // Arrange
        _quantity = DifferentQuantity;
        _other = GetMyType();

        // Act
        var actual = _myType.Equals(_other);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Equals_MyType_differentLabel_returns_false()
    {
        // Arrange
        _label = testLabel;
        _other = GetMyType();

        // Act
        var actual = _myType.Equals(_other);

        // Assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Equals_MyType_sameProperties_returns_true()
    {
        // Arrange
        _other = GetMyType();

        // Act
        var actual = _myType.Equals(_other);

        // Assert
        Assert.IsTrue(actual);
    }
    #endregion

    #region GetHashCode()
    [TestMethod]
    public void GetHashCode_differentQuantity_AreNotEqual()
    {
        // Arrange
        _quantity = DifferentQuantity;
        _other = GetMyType();

        // Act
        _hashCode1 = _myType.GetHashCode();
        _hashCode2 = _other.GetHashCode();

        // Assert
        Assert.AreNotEqual(_hashCode1, _hashCode2);
    }
    [TestMethod]
    public void GetHashCode_differentLabel_AreNotEqual()
    {
        // Arrange
        _label = testLabel;
        _other = GetMyType();

        // Act
        _hashCode1 = _myType.GetHashCode();
        _hashCode2 = _other.GetHashCode();

        // Assert
        Assert.AreNotEqual(_hashCode1, _hashCode2);
    }

    [TestMethod]
    public void GetHashCode_sameProperties_AreEqual()
    {
        // Arrange
        _other = GetMyType();

        // Act
        _hashCode1 = _myType.GetHashCode();
        _hashCode2 = _other.GetHashCode();

        // Assert
        Assert.AreEqual(_hashCode1, _hashCode2);
    }
    #endregion
    #endregion
    #endregion

    #region Dynamic data tests
    #region Members for dynamic data tests
    private string _testCase;
    private bool _expected;

    private static readonly MyTypeTests Instance = new();
    private const string DisplayName = nameof(GetDisplayName);

    private static IEnumerable<object[]> EqualsMyTypeArgs => Instance.GetEqualsMyTypeArgs();
    private static IEnumerable<object[]> EqualsObjectArgs => Instance.GetEqualsObjectArgs();
    private static IEnumerable<object[]> GetHashCodeArgs => Instance.GetGetHashCodeArgs();

    public static string GetDisplayName(MethodInfo testMethodInfo, object[] argsArray)
    {
        string testMethodName = testMethodInfo.Name;
        string testCase = (string)argsArray[0];

        return $"{testMethodName}: {testCase}";
    }
    #endregion

    #region Dynamic data test methods
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
    [DynamicData(nameof(GetHashCodeArgs), DynamicDataSourceType.Property, DynamicDataDisplayName = DisplayName)]
    public void GetHashCode_returns_expected(string testCase, bool expected, MyType myType, MyType other)
    {
        // Arrange
        _hashCode1 = myType.GetHashCode();
        _hashCode2 = other.GetHashCode();

        // Act
        var actual = _hashCode1 == _hashCode2;

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region Dynamic data sources
    private IEnumerable<object[]> GetGetHashCodeArgs()
    {
        InitMyType();

        _testCase = "Different Quantity, same Label => false";
        _quantity = DifferentQuantity;
        _other = GetMyType();
        _expected = false;
        yield return argsToObjectArray();

        _testCase = "Same Quantity, same Label => true";
        _quantity = TestQuantity;
        _other = GetMyType();
        _expected = true;
        yield return argsToObjectArray();

        _testCase = "Same Quantity, different Label => false";
        _label = testLabel;
        _other = GetMyType();
        _expected = false;
        yield return argsToObjectArray();

        #region argsToObjectArray
        object[] argsToObjectArray()
        {
            TestCase_bool_MyType_MyType args = new(_testCase, _expected, _myType, _other);

            return args.ToObjectArray();
        }
        #endregion
    }

    private IEnumerable<object[]> GetEqualsObjectArgs()
    {
        InitMyType();

        _testCase = "null => false";
        _obj = null;
        _expected = false;
        yield return argsToObjectArray();

        _testCase = "object => false";
        _obj = new();
        yield return argsToObjectArray();

        _testCase = "Same MyType => true";
        _obj = GetMyType();
        _expected = true;
        yield return argsToObjectArray();

        _testCase = "Different MyType => false";
        _quantity = DifferentQuantity;
        _label = testLabel;
        _obj = GetMyType();
        _expected = false;
        yield return argsToObjectArray();

        #region argsToObjectArray
        object[] argsToObjectArray()
        {
            TestCase_bool_MyType_object args = new(_testCase, _expected, _myType, _obj);

            return args.ToObjectArray();
        }
        #endregion
    }

    private IEnumerable<object[]> GetEqualsMyTypeArgs()
    {
        InitMyType();

        _testCase = "null => false";
        _other = null;
        _expected = false;

        TestCase_bool_MyType_MyType args = new(_testCase, _expected, _myType, _other);
        object[] nullMyTypeArgsArray = args.ToObjectArray();

        return GetHashCodeArgs.Append(nullMyTypeArgsArray);
    }
    #endregion
    #endregion
}

#nullable enable