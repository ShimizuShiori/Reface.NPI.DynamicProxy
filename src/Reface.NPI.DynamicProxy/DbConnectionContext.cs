using System.Data;

namespace Reface.NPI.DynamicProxy
{
    /// <summary>
    /// 数据库连接上下文
    /// </summary>
    public class DbConnectionContext
    {
        public IDbConnection DbConnection { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        public DbConnectionContext(IDbConnection dbConnection, IDbTransaction transaction)
        {
            DbConnection = dbConnection;
            Transaction = transaction;
        }

        public DbConnectionContext(IDbConnection dbConnection) : this(dbConnection, null)
        {

        }
    }
}
