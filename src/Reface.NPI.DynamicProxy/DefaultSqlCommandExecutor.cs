using Dapper;

namespace Reface.NPI.DynamicProxy
{
    public class DefaultSqlCommandExecutor : ISqlCommandExecutor
    {
        public int Execute(string sqlCommand, DapperParameters paras, DbConnectionContext context)
        {
            return context.DbConnection.Execute(sqlCommand, paras, transaction: context.Transaction);
        }
    }
}
