using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Reface.NPI.DynamicProxy.DbReturnValueHandlers
{
    public class ListDbReturnValueHandler : IDbReturnValueHandler
    {
        public bool CanHandle(MethodInfo methodInfo, Type entityType)
        {
            Type returnType = methodInfo.ReturnType;
            return returnType.GetInterface(typeof(IList).FullName) != null;
        }

        public object Handle(MethodInfo methodInfo, Type entityType, object dbReturnedValue)
        {
            Type type = typeof(List<>).MakeGenericType(new Type[] { entityType });
            object result = Activator.CreateInstance(type);
            IList list = (IList)dbReturnedValue;
            foreach (var x in list)
                ((IList)result).Add(x);
            return result;
        }
    }
}
