

Type type = typeof(Program);
Type? typeNull = null;
Console.WriteLine(type == typeNull);




// C#11 语法, 至少3个双引号(""")开头和结尾，内容可以输入任何原始字符
// 单行: 左引号，右引号，内容 三者同行
string singleLine = """Content begin "Hello World!" end.""";
string singleLine1 = """"Content begin "Hello World!" """ end."""";

// 多行：左引号，右引号各一行，内容需与右引号缩进对齐
string multiLine = """
    Content begin "Hello World!" /\n<>"" end.
    """;
Console.WriteLine(multiLine); // => Content begin "Hello World!" /\n<>"" end.


var sb = new System.Text.StringBuilder();
for (int i = 0; i < 100; i++)
{
    sb.Append(i.ToString());
}
Console.WriteLine(sb);
Console.WriteLine(sb.ToString());

/////////////////////////

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