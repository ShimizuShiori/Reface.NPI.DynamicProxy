using System.Data;

namespace Reface.NPI.DynamicProxy
{
    /// <summary>
    /// 数据库连接上下文
    /// </summary>
    public class DbConnectionContext
    {
        public IDbConnection DbConnection { get; private set; }
        public IDbTransaction Transaction { get; set; }

        public DbConnectionContext(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        //public void BeginTran()
        //{
        //    this.Transaction = this.DbConnection.BeginTransaction();
        //}

        //public void Rollback()
        //{
        //    this.Transaction.Rollback();
        //    this.Transaction.Dispose();
        //    this.Transaction = null;
        //}

        //public void Commit()
        //{
        //    this.Transaction.Commit();
        //    this.Transaction.Dispose();
        //    this.Transaction = null;
        //}
    }
}
