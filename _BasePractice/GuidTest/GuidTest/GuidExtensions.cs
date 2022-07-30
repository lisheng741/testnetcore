using Simple.Common.Guids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System;

public static class GuidExtensions
{
    public static Guid Next()
    {
        return GuidHelper.Next();
    }
}
