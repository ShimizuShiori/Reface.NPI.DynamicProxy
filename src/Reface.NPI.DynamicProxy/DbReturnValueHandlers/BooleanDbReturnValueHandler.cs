using System;
using System.Reflection;

namespace Reface.NPI.DynamicProxy.DbReturnValueHandlers
{
    public class BooleanDbReturnValueHandler : IDbReturnValueHandler
    {
        public bool CanHandle(MethodInfo methodInfo, Type entityType)
        {
            return methodInfo.ReturnType == typeof(Boolean);
        }

        public object Handle(MethodInfo methodInfo, Type entityType, object dbReturnedValue)
        {
            return (int)dbReturnedValue > 0;
        }
    }
}
