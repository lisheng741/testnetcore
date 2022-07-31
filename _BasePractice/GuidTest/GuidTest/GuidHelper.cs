using System.Security.Cryptography;

namespace Simple.Common.Guids;

public enum SequentialGuidType
{
    /// <summary>
    /// 用以 SqlServer.
    /// 连续性体现于 GUID 的第4块（Data4）.
    /// </summary>
    AtEnd,

    /// <summary>
    /// 用于 MySql 和 PostgreSql.
    ///  当使用 <see cref="Guid.ToString()" /> 方法进行格式化时连续.
    /// </summary>
    AsString,

    /// <summary>
    /// 用于 Oracle.
    /// 当使用 <see cref="Guid.ToByteArray" /> 方法进行格式化时连续.
    /// </summary>
    AsBinary
}

public static class GuidHelper
{

    private static readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

    public static SequentialGuidType SequentialGuidType { get; set; } = SequentialGuidType.AtEnd;

    /// <summary>
    /// 生成连续 Guid.
    /// </summary>
    /// <returns></returns>
    public static Guid Next()
    {
        return Next(SequentialGuidType);
    }

    public static Guid NextNew()
    {
        return NextNew(SequentialGuidType.AtEnd);
    }

    /// <summary>
    /// 生成连续 Guid.
    /// 来源：Abp.进行了一定的调整
    /// </summary>
    /// <param name="guidType"></param>
    /// <returns></returns>
    public static Guid Next(SequentialGuidType guidType)
    {
        var randomBytes = new byte[8];
        _randomNumberGenerator.GetBytes(randomBytes);

        long timestamp = DateTime.UtcNow.Ticks;

        byte[] timestampBytes = BitConverter.GetBytes(timestamp);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(timestampBytes);
        }

        byte[] guidBytes = new byte[16];

        switch (guidType)
        {
            case SequentialGuidType.AsString:
            case SequentialGuidType.AsBinary:

                // 16位数组：前6位为时间戳，后10位为随机数
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 0, 8);
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 8, 8);

                // If formatting as a string, we have to compensate for the fact
                // that .NET regards the Data1 and Data2 block as an Int32 and an Int16,
                // respectively.  That means that it switches the order on little-endian
                // systems.  So again, we have to reverse.
                if (guidType == SequentialGuidType.AsString && BitConverter.IsLittleEndian)
                {
                    Array.Reverse(guidBytes, 0, 4);
                    Array.Reverse(guidBytes, 4, 2);
                    Array.Reverse(guidBytes, 6, 2);
                }

                break;

            case SequentialGuidType.AtEnd:

                // 16位数组：前10位为随机数，后6位为时间戳
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 8);
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 8, 8);
                break;
        }

        return new Guid(guidBytes);
    }

    public static Guid NextNew(SequentialGuidType guidType)
    {
        // see: What is a GUID? http://guid.one/guid
        // from: https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/blob/ebe011a6f1b2a2a9709fe558cfc7ed3215b55c37/src/EFCore.MySql/ValueGeneration/Internal/MySqlSequentialGuidValueGenerator.cs
        // According to RFC 4122:
        // AsString: dddddddd-dddd-Mddd-Ndrr-rrrrrrrrrrrr
        // AtEnd:    rrrrrrrr-rrrr-Mrrd-Nddd-dddddddddddd
        // - M = RFC version, in this case '4' for random UUID
        // - N = RFC variant (plus other bits), in this case 0b1000 for variant 1
        // - d = nibbles based on UTC date/time in ticks
        // - r = nibbles based on random bytes

        var randomBytes = new byte[8];
        _randomNumberGenerator.GetBytes(randomBytes);

        // see: https://github.com/richardtallent/RT.Comb#gory-details-about-uuids-and-guids
        uint data1;
        ushort data2, data3;
        byte[] data4 = new byte[8];
        byte version = (byte)4;
        byte variant = (byte)8;
        byte filterHighBit = 0b00001111;
        byte filterLowBit = 0b11110000;

        long timestamp = DateTime.UtcNow.Ticks;

        byte[] timestampBytes = BitConverter.GetBytes(timestamp);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(timestampBytes);
        }

        byte[] guidBytes = new byte[16];

        switch (guidType)
        {
            case SequentialGuidType.AsString:
            case SequentialGuidType.AsBinary:

                //// 方案1：
                //data1 = (uint)(timestamp >> 32);
                //data2 = (ushort)(timestamp >> 16);
                //data3 = (ushort)((timestamp >> 4) | (ushort)(version << 12));
                //data4[0] = (byte)(timestamp | (byte)(variant << 4));
                //Buffer.BlockCopy(randomBytes, 0, data4, 1, 7); // 跳过data4的1个字节，从randomBytes第0个字节开始复制7个字节

                // 方案2：
                // AsString: dddddddd-dddd-Mddd-Ndrr-rrrrrrrrrrrr
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 0, 6); // 时间戳前6个字节，共48位
                guidBytes[6] = (byte)((version << 4) | ((timestampBytes[6] & filterLowBit) >> 4)); // 高4位为版本 | 低4位取时间戳序号[6]的元素的高4位
                guidBytes[7] = (byte)(((timestampBytes[6] & filterHighBit) << 4) | ((timestampBytes[7] & filterLowBit) >> 4)); // 高4位取：[6]低4位 | 低4位取：[7]高4位
                guidBytes[8] = (byte)((variant << 4) | (timestampBytes[7] & filterHighBit)); // 高4位为：变体 | 低4位取：[7]低4位
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 9, 7); // 余下7个字节由随机数组填充

                // .NET中，Data1、Data2、Data3 块 分别视为 Int32、Int16、Int16，在小端系统，需要翻转这3个块。
                if (guidType == SequentialGuidType.AsString && BitConverter.IsLittleEndian)
                {
                    Array.Reverse(guidBytes, 0, 4);
                    Array.Reverse(guidBytes, 4, 2);
                    Array.Reverse(guidBytes, 6, 2);
                }

                #region test
                //Console.WriteLine($"{new Guid(guidBytes)}");


                //Buffer.BlockCopy(timestampBytes, 0, guidBytes, 0, 8);
                //Buffer.BlockCopy(randomBytes, 0, guidBytes, 8, 8);

                //// If formatting as a string, we have to compensate for the fact
                //// that .NET regards the Data1 and Data2 block as an Int32 and an Int16,
                //// respectively.  That means that it switches the order on little-endian
                //// systems.  So again, we have to reverse.
                //if (guidType == SequentialGuidType.AsString && BitConverter.IsLittleEndian)
                //{
                //    Array.Reverse(guidBytes, 0, 4);
                //    Array.Reverse(guidBytes, 4, 2);
                //    Array.Reverse(guidBytes, 6, 2);
                //}

                //Console.WriteLine($"{new Guid(guidBytes)}");
                #endregion

                break;

            case SequentialGuidType.AtEnd:

                // AtEnd:    rrrrrrrr-rrrr-Mdrr-Nddd-dddddddddddd
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 6);
                guidBytes[6] = (byte)((version << 4) | (timestampBytes[7] & filterHighBit)); // 高4位为版本 | 低4位取：时间戳[7]低4位
                guidBytes[7] = randomBytes[7]; // 随机数
                guidBytes[8] = (byte)((variant << 4) | ((timestampBytes[6] & filterLowBit) >> 4)); // 高4位为：变体 | 低4位取：时间戳[6]高4位
                guidBytes[9] = (byte)(((timestampBytes[6] & filterHighBit) << 4) | ((timestampBytes[7] & filterLowBit) >> 4)); //高4位为：时间戳[6]低4位 | 低4位取：时间戳[7]高4位
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 10, 6); // 时间戳前6个字节

                if (BitConverter.IsLittleEndian)
                {
                    //Array.Reverse(guidBytes, 0, 4); // 随机数
                    //Array.Reverse(guidBytes, 4, 2);
                    Array.Reverse(guidBytes, 6, 2);
                }
                break;
        }

        return new Guid(guidBytes);
    }

    // from: abp
    // from jhtodd/SequentialGuid https://github.com/jhtodd/SequentialGuid/blob/master/SequentialGuid/Classes/SequentialGuid.cs .
    public static Guid Old()
    {
        var randomBytes = new byte[10];
        _randomNumberGenerator.GetBytes(randomBytes);

        long timestamp = DateTime.UtcNow.Ticks;

        byte[] timestampBytes = BitConverter.GetBytes(timestamp);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(timestampBytes);
        }

        byte[] guidBytes = new byte[16];

        // 16位数组：前6位为时间戳，后10位为随机数
        Buffer.BlockCopy(timestampBytes, 0, guidBytes, 0, 6);
        Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

        // If formatting as a string, we have to compensate for the fact
        // that .NET regards the Data1 and Data2 block as an Int32 and an Int16,
        // respectively.  That means that it switches the order on little-endian
        // systems.  So again, we have to reverse.
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(guidBytes, 0, 4);
            Array.Reverse(guidBytes, 4, 2);
        }

        return new Guid(guidBytes);
    }

    // from: furion
    // from: https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/blob/ebe011a6f1b2a2a9709fe558cfc7ed3215b55c37/src/EFCore.MySql/ValueGeneration/Internal/MySqlSequentialGuidValueGenerator.cs
    public static Guid Create(object idGeneratorOptions = null)
    {
        // According to RFC 4122:
        // dddddddd-dddd-Mddd-Ndrr-rrrrrrrrrrrr
        // - M = RFC version, in this case '4' for random UUID
        // - N = RFC variant (plus other bits), in this case 0b1000 for variant 1
        // - d = nibbles based on UTC date/time in ticks
        // - r = nibbles based on random bytes

        var randomBytes = new byte[7];
        _randomNumberGenerator.GetBytes(randomBytes);
        var ticks = (ulong)DateTimeOffset.UtcNow.Ticks;

        var uuidVersion = (ushort)4;
        var uuidVariant = (ushort)0b1000;

        var ticksAndVersion = (ushort)((ticks << 48 >> 52) | (ushort)(uuidVersion << 12));
        var ticksAndVariant = (byte)((ticks << 60 >> 60) | (byte)(uuidVariant << 4));

        if (true)
        {
            var guidBytes = new byte[16];
            var tickBytes = BitConverter.GetBytes(ticks);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(tickBytes);
            }

            Buffer.BlockCopy(tickBytes, 0, guidBytes, 0, 6);
            guidBytes[6] = (byte)(ticksAndVersion << 8 >> 8);
            guidBytes[7] = (byte)(ticksAndVersion >> 8);
            guidBytes[8] = ticksAndVariant;
            Buffer.BlockCopy(randomBytes, 0, guidBytes, 9, 7);

            return new Guid(guidBytes);
        }

        var guid = new Guid((uint)(ticks >> 32), (ushort)(ticks << 32 >> 48), ticksAndVersion,
            ticksAndVariant,
            randomBytes[0],
            randomBytes[1],
            randomBytes[2],
            randomBytes[3],
            randomBytes[4],
            randomBytes[5],
            randomBytes[6]);

        return guid;
    }
}
