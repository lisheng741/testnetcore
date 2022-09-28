using Simple.Common.Helpers;

namespace System;

public static class GuidExtensions
{
    public static Guid Next()
    {
        return GuidHelper.Next();
    }
}
