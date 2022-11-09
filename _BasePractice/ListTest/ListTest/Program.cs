
var list = new List<object>()
{
    "11", "222", "333"
};

list.ForEach(x =>
{
    x = x.ToString() + "aaa";
    Console.WriteLine(x);
});

list.ForEach(x => Console.WriteLine(x));
