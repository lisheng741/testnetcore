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

    public static SequentialGuidType SequentialGuidType { get; set; } = SequentialGuidType.AsString; // SequentialGuidType.AtEnd;

    /// <summary>
    /// 生成连续 Guid.
    /// </summary>
    /// <returns></returns>
    public static Guid Next()
    {
        return Next(SequentialGuidType);
    }

    /// <summary>
    /// 生成连续 Guid.
    /// 来源：Abp
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

        if (false)
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
