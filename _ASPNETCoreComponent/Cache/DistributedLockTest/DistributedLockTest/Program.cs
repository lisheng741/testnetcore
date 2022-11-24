
using DistributedLockTest;

string lockKey = "lock:eat";
TimeSpan expiration = TimeSpan.FromSeconds(5);

Parallel.For(0, 5, async x =>
{
    string person = $"person:{x}";
    bool isLocked = RedisManager.AcquireLock(lockKey, person, expiration);
    var val = 0;

    while (!isLocked && val <= 2000)
    {
        val += 250;
        await Task.Delay(250);
        isLocked = RedisManager.AcquireLock(lockKey, person, expiration);
        Console.WriteLine($"{x} acquire lock at {Thread.CurrentThread.ManagedThreadId}.: {isLocked}");
    }

    if (isLocked)
    {
        Console.WriteLine($"{person} begin eat food(with lock) at {Thread.CurrentThread.ManagedThreadId}.");
        await Task.Delay(600);
        RedisManager.ReleaseLock(lockKey, person);
        Console.WriteLine($"{x} release lock at {Thread.CurrentThread.ManagedThreadId}.");
    }
    else
    {
        Console.WriteLine($"{person} can not eat food due to don't get the lock. at {Thread.CurrentThread.ManagedThreadId}");
    }
});

Console.WriteLine("end");

//Parallel.For(0, 5, x =>
//{
//    string person = $"person:{x}";
//    bool isLocked = RedisManager.AcquireLock(lockKey, person, expiration);
//    var val = 0;

//    while (!isLocked && val <= 2000)
//    {
//        val += 250;
//        Thread.Sleep(250);
//        isLocked = RedisManager.AcquireLock(lockKey, person, expiration);
//        Console.WriteLine($"{x} acquire lock at {Thread.CurrentThread.ManagedThreadId}.: {isLocked}");
//    }

//    if (isLocked)
//    {
//        Console.WriteLine($"{person} begin eat food(with lock) at {Thread.CurrentThread.ManagedThreadId}.");
//        Thread.Sleep(1000);
//        RedisManager.ReleaseLock(lockKey, person);
//        Console.WriteLine($"{x} release lock at {Thread.CurrentThread.ManagedThreadId}.");
//    }
//    else
//    {
//        Console.WriteLine($"{person} can not eat food due to don't get the lock. at {Thread.CurrentThread.ManagedThreadId}");
//    }
//});

Console.ReadLine();
