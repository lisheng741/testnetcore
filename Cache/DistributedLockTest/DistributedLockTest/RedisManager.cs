using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLockTest;

internal class RedisManager
{
    private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
    {
        var configuration = new ConfigurationOptions()
        {
            AbortOnConnectFail = false,
            ConnectTimeout = 5000,
            Password = "123456"
        };

        configuration.EndPoints.Add("127.0.0.1", 6379);

        return ConnectionMultiplexer.Connect(configuration.ToString());
    });

    public static ConnectionMultiplexer Connection = lazyConnection.Value;

    public static bool AcquireLock(string key, string value, TimeSpan expiration)
    {
        bool flag = false;

        try
        {
            flag = Connection.GetDatabase().StringSet(key, value, expiration, when: When.NotExists);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Acquire lock fail...{ex.Message}");
            flag = true;
        }

        return flag;
    }

    public static void ReleaseLock(string key, string value)
    {
        string lua_script = @"  
            if (redis.call('GET', KEYS[1]) == ARGV[1]) then  
                redis.call('DEL', KEYS[1])  
                return true  
            else  
                return false  
            end  
            ";

        Connection.GetDatabase().ScriptEvaluate(lua_script, new RedisKey[] { key }, new RedisValue[] { value });
    }
}
