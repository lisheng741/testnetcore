using Castle.DynamicProxy;

namespace Simple.Common.Components.Castle;

public static class CastleHelper
{
    /// <summary>
    /// ProxyGenerator 实例
    /// </summary>
    public static ProxyGenerator Generator { get; set; } = new ProxyGenerator();
}
