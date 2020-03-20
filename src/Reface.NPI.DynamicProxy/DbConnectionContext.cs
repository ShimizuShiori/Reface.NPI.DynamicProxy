using System.Data;

namespace Reface.NPI.DynamicProxy
{
    /// <summary>
    /// 数据库连接上下文
    /// </summary>
    public class DbConnectionContext
    {
        public IDbConnection DbConnection { get; private set; }

        /// <summary>
        /// 使用 BeginTran 后，此属性会被设置一个事务，当事务提交后，会被置为 null
        /// </summary>
        public IDbTransaction Transaction { get; private set; }

        public DbConnectionContext(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public IDbTransaction BeginTran()
        {
            var proxy = new DbTranscationProxy(this.DbConnection.BeginTransaction());
            this.Transaction = proxy;
            proxy.Disposing += Proxy_Disposing;
            return proxy;
        }
        private void Proxy_Disposing(object sender, System.EventArgs e)
        {
            this.Transaction = null;
        }
    }
}
