using System;
using System.Collections;
using System.Reflection;

namespace Reface.NPI.DynamicProxy.DbReturnValueHandlers
{
    public class SingleEntityDbReturnValueHandler : IDbReturnValueHandler
    {
        public bool CanHandle(MethodInfo methodInfo, Type entityType)
        {
            return methodInfo.ReturnType == entityType;
        }

        public object Handle(MethodInfo methodInfo, Type entityType, object dbReturnedValue)
        {
            var list = (IList)dbReturnedValue;
            var first = list[0];
            return first;
        }
    }
}
