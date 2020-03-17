using System;
using System.Reflection;

namespace Reface.NPI.DynamicProxy.DbReturnValueHandlers
{
    public class VoidDbReturnValueHandler : IDbReturnValueHandler
    {
        public bool CanHandle(MethodInfo methodInfo, Type entityType)
        {
            return methodInfo.ReturnType == typeof(void);
        }

        public object Handle(MethodInfo methodInfo, Type entityType, object dbReturnedValue)
        {
            return null;
        }
    }
}
