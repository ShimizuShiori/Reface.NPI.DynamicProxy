using Reface.NPI.DynamicProxy.DbReturnValueHandlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.NPI.DynamicProxy
{
    public class ServicesCollection
    {
        private readonly static Dictionary<Type, List<Func<Type, Object>>> factories = new Dictionary<Type, List<Func<Type, object>>>();

        static ServicesCollection()
        {
            RegisterService<ISqlCommandExecutor>(t => new DefaultSqlCommandExecutor());
            RegisterService<ISqlCommandQuerier>(t => new DefaultSqlCommandQuerier());
            RegisterService<IDbReturnValueHandler>(t => new ListDbReturnValueHandler());
            RegisterService<IDbReturnValueHandler>(t => new BooleanDbReturnValueHandler());
            RegisterService<IDbReturnValueHandler>(t => new VoidDbReturnValueHandler());
            RegisterService<IDbReturnValueHandler>(t => new SingleEntityDbReturnValueHandler());
        }

        public static void RegisterService<T>(Func<Type, T> factory)
        {
            Type type = typeof(T);
            List<Func<Type, Object>> fs;
            if (!factories.TryGetValue(type, out fs))
            {
                fs = new List<Func<Type, object>>();
                factories[type] = fs;
            }
            fs.Add(x => factory(type));
        }

        public static void ReplaceService<T>(Func<Type, T> factory)
        {
            List<Func<Type, object>> fs = new List<Func<Type, object>>();
            fs.Add(x => factory(typeof(T)));
            factories[typeof(T)] = fs;
        }

        public static T GetService<T>()
        {
            List<Func<Type, Object>> fs;
            if (!factories.TryGetValue(typeof(T), out fs))
                throw new KeyNotFoundException("未注册的组件");

            if (fs.Count() > 1)
                throw new IndexOutOfRangeException("注册有多个组件，请使用 GetServices");

            return (T)fs.First()(typeof(T));
        }

        public static IEnumerable<T> GetServices<T>()
        {
            List<Func<Type, Object>> fs;
            if (!factories.TryGetValue(typeof(T), out fs))
                throw new KeyNotFoundException("未注册的组件");

            return fs.Select(x => x(typeof(T))).Cast<T>();
        }
    }
}
