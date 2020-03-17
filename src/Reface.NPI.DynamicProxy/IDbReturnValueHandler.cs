using System;
using System.Reflection;

namespace Reface.NPI.DynamicProxy
{
    /// <summary>
    /// 对数据库返回的结果的处理器
    /// </summary>
    public interface IDbReturnValueHandler
    {
        bool CanHandle(MethodInfo methodInfo, Type entityType);

        object Handle(MethodInfo methodInfo, Type entityType, object dbReturnedValue);
    }
}
