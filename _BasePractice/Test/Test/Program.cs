

Func<List<int>, int, List<int>> intSet = Set;

List<int> ints = new List<int>();

Parallel.For(0, 5, x =>
{
    for(int i = 0; i < x; i++)
    {
        intSet(ints, x);
    }
});

ints.ForEach(i => Console.WriteLine(i));

static List<int> Set(List<int> list, int i)
{
    list.Add(i);
    return list;
}

// 数组每个元素+1
int[] intArray = { 1, 2, 3, 4, 5 };
foreach (ref int i in intArray.AsSpan())
{
    i++;
}
Console.WriteLine(String.Join(",", intArray));

// 字符串长度
string s = "2022061";

if (s.Length != 6) throw new Exception();

Console.WriteLine(s.Substring(0, 4));

Console.WriteLine(s.Substring(4, 2));

Console.ReadLine();



// 逆变 协变
object o = s;
IEnumerable<string> strings = new List<string>();
IEnumerable<object> objects = strings;

OutTest<string> testClass = new TestClass<string>("sss123");
OutTest<object> objTest = testClass;


// 协变
public interface OutTest<out T>
{
    T Value { get; }
}

public class TestClass<T> : OutTest<T>
{
    public T Value { get; set; }

    public TestClass(T value)
    {
        Value = value;
    }
}