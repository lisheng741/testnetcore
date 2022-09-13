
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

Console.WriteLine("-----------------");

DateTime dt = DateTime.Now;
DateTimeOffset df = DateTimeOffset.Now;

DateTime dt2 = df.LocalDateTime;
DateTimeOffset df2 = dt;

Console.WriteLine(dt2);
Console.WriteLine(df2);

Console.ReadLine();
