using Castle.DynamicProxy;

namespace Reface.NPI.DynamicProxy
{
    public class SqlServerNPIImplementer : INPIImplementer
    {
        private readonly DbConnectionContext dbConnectionContext;

        public SqlServerNPIImplementer(DbConnectionContext dbConnectionContext)
        {
            this.dbConnectionContext = dbConnectionContext;
        }

        public T Implement<T>()
            where T : class
        {
            ProxyGenerator pg = new ProxyGenerator();
            return pg.CreateInterfaceProxyWithoutTarget<T>(new NPIInterceptor(this.dbConnectionContext));
        }
    }
}
