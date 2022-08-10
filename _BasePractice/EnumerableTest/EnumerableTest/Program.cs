
Console.WriteLine(typeof(int[]).IsAssignableFrom(typeof(IEnumerable<int>)));

Console.WriteLine(typeof(IList<int>).IsAssignableFrom(typeof(int[])));

Console.WriteLine(typeof(IEnumerable<int>).IsAssignableFrom(typeof(int[])));

Console.WriteLine(typeof(IEnumerable<int>).IsAssignableFrom(typeof(List<int>)));

Console.ReadLine();
