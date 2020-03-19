using Castle.DynamicProxy;

namespace Reface.NPI.DynamicProxy
{
    public class NPIImplementer : INPIImplementer
    {
        private readonly DbConnectionContext dbConnectionContext;

        public NPIImplementer(DbConnectionContext dbConnectionContext)
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
