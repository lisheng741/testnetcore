﻿using System.Security.Cryptography;

namespace Simple.Common.Guids;

public enum SequentialGuidType
{
    /// <summary>
    /// 用于 MySql 和 PostgreSql.
    ///  当使用 <see cref="Guid.ToString()" /> 方法进行格式化时连续.
    /// </summary>
    AsString,

    /// <summary>
    /// 用于 Oracle.
    /// 当使用 <see cref="Guid.ToByteArray()" /> 方法进行格式化时连续.
    /// </summary>
    AsBinary,

    /// <summary>
    /// 用以 SqlServer.
    /// 连续性体现于 GUID 的第4块（Data4）.
    /// </summary>
    AtEnd
}

public static class GuidHelper
{
    private static readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();
    private const byte version = (byte)4;
    private const byte variant = (byte)8;
    private const byte filterHighBit = 0b00001111;
    private const byte filterLowBit = 0b11110000;

    /// <summary>
    /// 连续 Guid 类型，默认：AsString.
    /// </summary>
    public static SequentialGuidType SequentialGuidType { get; set; } = SequentialGuidType.AsString;

    /// <summary>
    /// 生成连续 Guid.
    /// </summary>
    /// <returns></returns>
    public static Guid Next()
    {
        return Next(SequentialGuidType);
    }

    public static Guid Create(SequentialGuidType guidType)
    {
        // We start with 16 bytes of cryptographically strong random data.
        byte[] randomBytes = new byte[10];
        _randomNumberGenerator.GetBytes(randomBytes);

        long timestamp = DateTime.UtcNow.Ticks / 10000L;

        // Then get the bytes
        byte[] timestampBytes = BitConverter.GetBytes(timestamp);

        // Since we're converting from an Int64, we have to reverse on
        // little-endian systems.
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(timestampBytes);
        }

        byte[] guidBytes = new byte[16];

        switch (guidType)
        {
            case SequentialGuidType.AsString:
            case SequentialGuidType.AsBinary:

                // For string and byte-array version, we copy the timestamp first, followed
                // by the random data.
                Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                // If formatting as a string, we have to compensate for the fact
                // that .NET regards the Data1 and Data2 block as an Int32 and an Int16,
                // respectively.  That means that it switches the order on little-endian
                // systems.  So again, we have to reverse.
                if (guidType == SequentialGuidType.AsString && BitConverter.IsLittleEndian)
                {
                    Array.Reverse(guidBytes, 0, 4);
                    Array.Reverse(guidBytes, 4, 2);
                }

                break;

            case SequentialGuidType.AtEnd:

                // For sequential-at-the-end versions, we copy the random data first,
                // followed by the timestamp.
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                break;
        }

        return new Guid(guidBytes);
    }

    /// <summary>
    /// 生成连续 Guid（生成的 Guid 并不符合 RFC 4122 标准）.
    /// 来源：Abp. from jhtodd/SequentialGuid https://github.com/jhtodd/SequentialGuid/blob/master/SequentialGuid/Classes/SequentialGuid.cs .
    /// </summary>
    /// <param name="guidType"></param>
    /// <returns></returns>
    public static Guid NextOld(SequentialGuidType guidType)
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

                // 16位数组：前8位为时间戳，后8位为随机数
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 0, 8);
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 8, 8);

                // .NET中，Data1、Data2、Data3 块 分别视为 Int32、Int16、Int16，在小端系统，需要翻转这3个块。
                if (guidType == SequentialGuidType.AsString && BitConverter.IsLittleEndian)
                {
                    Array.Reverse(guidBytes, 0, 4);
                    Array.Reverse(guidBytes, 4, 2);
                    Array.Reverse(guidBytes, 6, 2);
                }

                break;

            case SequentialGuidType.AtEnd:

                // 16位数组：前8位为随机数，后8位为时间戳
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 8);
                //Buffer.BlockCopy(timestampBytes, 0, guidBytes, 8, 8);
                Buffer.BlockCopy(timestampBytes, 6, guidBytes, 8, 2);
                Buffer.BlockCopy(timestampBytes, 0, guidBytes, 10, 6);
                break;
        }

        return new Guid(guidBytes);
    }

    /// <summary>
    /// 生成连续 Guid.
    /// </summary>
    /// <param name="guidType"></param>
    /// <returns></returns>
    public static Guid Next(SequentialGuidType guidType)
    {
        // see: What is a GUID? http://guid.one/guid
        // see: https://github.com/richardtallent/RT.Comb#gory-details-about-uuids-and-guids
        // According to RFC 4122:
        // xxxxxxxx-xxxx-Mxxx-Nxxx-xxxxxxxxxxxx
        // AsString: dddddddd-dddd-Mddd-Ndrr-rrrrrrrrrrrr
        // - M = RFC 版本（version）, 版本4的话，值为4
        // - N = RFC 变体（variant），值为 8, 9, A, B 其中一个，这里固定为8
        // - d = 从公元1年1月1日0时至今的时钟周期数（DateTime.UtcNow.Ticks）
        // - r = 随机数（random bytes）

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

                break;

            case SequentialGuidType.AtEnd:

                // AtEnd:    rrrrrrrr-rrrr-Mxdr-Nddd-dddddddddddd
                // SqlServer
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 6);
                guidBytes[6] = (byte)(version << 4); // Mx 高4位为版本 | 低4位取：全0
                guidBytes[7] = (byte)(((timestampBytes[7] & filterHighBit) << 4) | (randomBytes[7] & filterHighBit)); ; // dr 高4位为：时间戳[7]低4位 | 低4位取：随机数
                guidBytes[8] = (byte)((variant << 4) | ((timestampBytes[6] & filterLowBit) >> 4)); // Nd 高4位为：变体 | 低4位取：时间戳[6]高4位
                guidBytes[9] = (byte)(((timestampBytes[6] & filterHighBit) << 4) | ((timestampBytes[7] & filterLowBit) >> 4)); // dd 高4位为：时间戳[6]低4位 | 低4位取：时间戳[7]高4位
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
}
