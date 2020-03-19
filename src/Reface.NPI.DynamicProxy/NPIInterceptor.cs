using Castle.Core.Interceptor;
using Reface.NPI.Generators;
using System;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy
{
    public class NPIInterceptor : IInterceptor
    {
        private readonly DbConnectionContext dbConnectionContext;

        private static IEnumerable<IDbReturnValueHandler> dbReturnValueHandlers;

        static NPIInterceptor()
        {
            dbReturnValueHandlers = ServicesCollection.GetServices<IDbReturnValueHandler>();
        }

        public NPIInterceptor(DbConnectionContext dbConnectionContext)
        {
            this.dbConnectionContext = dbConnectionContext;
        }

        public void Intercept(IInvocation invocation)
        {
            string methodName = invocation.Method.Name;

            Type typeofIDao = typeof(INpiDao<>);
            Type entityType = invocation.Method.DeclaringType.GetInterface(typeofIDao.FullName).GetGenericArguments()[0];


            // todo :对其单例化
            ISqlCommandGenerator g = NpiServicesCollection.GetService<ISqlServerCommandGenerator>();
            SqlCommandDescription d = g.Generate(invocation.Method, invocation.Arguments);
            DebugLogger.Debug(d.ToString());

            DapperParameters dp = new DapperParameters();
            foreach (var p in d.Parameters)
            {
                dp[p.Key] = p.Value.Value;
            }

            object dbReturnedValue = null;
            object handledValue = null;

            switch (d.Type)
            {
                case SqlCommandTypes.Insert:
                case SqlCommandTypes.Update:
                case SqlCommandTypes.Delete:
                    {
                        var executor = ServicesCollection.GetService<ISqlCommandExecutor>();
                        dbReturnedValue = executor.Execute(d.SqlCommand, dp, dbConnectionContext);
                    }
                    break;
                case SqlCommandTypes.Select:
                    {
                        var querier = ServicesCollection.GetService<ISqlCommandQuerier>();
                        dbReturnedValue = querier.Select(entityType, d.SqlCommand, dp, dbConnectionContext);
                    }
                    break;
                default:
                    break;
            }
            foreach (var handler in dbReturnValueHandlers)
            {
                if (!handler.CanHandle(invocation.Method, entityType)) continue;
                handledValue = handler.Handle(invocation.Method, entityType, dbReturnedValue);
            }
            invocation.ReturnValue = handledValue;
        }
    }
}
