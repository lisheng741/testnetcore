
Console.WriteLine(DateTime.Now);
Console.WriteLine(DateTime.UtcNow);

Console.WriteLine(DateTime.Now.Ticks);
Console.WriteLine(DateTime.UtcNow.Ticks);

Console.WriteLine("-----------------");

Console.WriteLine(DateTimeOffset.Now);
Console.WriteLine(DateTimeOffset.UtcNow);

Console.WriteLine(DateTimeOffset.Now.Ticks);
Console.WriteLine(DateTimeOffset.UtcNow.Ticks);

Console.WriteLine("-----------------");
Console.WriteLine("-----------------");

Console.WriteLine(DateTimeOffset.UtcNow.ToLocalTime());
Console.WriteLine(DateTimeOffset.Now.UtcDateTime);

Console.ReadLine();
