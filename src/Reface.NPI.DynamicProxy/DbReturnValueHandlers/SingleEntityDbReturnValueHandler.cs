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
            if (dbReturnedValue == null) return null;
            var list = (IList)dbReturnedValue;
            if (list.Count == 0) return null;
            var first = list[0];
            return first;
        }
    }
}
