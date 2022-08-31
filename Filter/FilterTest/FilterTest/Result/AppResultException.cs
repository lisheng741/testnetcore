using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common;

/// <summary>
/// 返沪统一结果的异常
/// </summary>
public class AppResultException : Exception
{
    /// <summary>
    /// 结果信息
    /// </summary>
    public AppResult AppResult { get; private set; }

    /// <summary>
    /// 源异常
    /// </summary>
    public Exception? SourceException { get; private set; }

    public AppResultException()
        : this(new AppResult(), null)
    {
    }

    public AppResultException(AppResult result)
        : this(result, null)
    {
    }

    public AppResultException(Exception exception)
        : this(new AppResult(), exception)
    {
    }

    public AppResultException(AppResult result, Exception? exception)
    {
        AppResult = result;
        SourceException = exception;
    }
}
