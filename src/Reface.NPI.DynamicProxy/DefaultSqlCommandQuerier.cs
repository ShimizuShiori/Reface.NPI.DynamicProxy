using Dapper;
using System;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy
{
    public class DefaultSqlCommandQuerier : ISqlCommandQuerier
    {
        public IEnumerable<object> Select(Type entityType, string sqlCommand, DapperParameters paras, DbConnectionContext context)
        {
            return context.DbConnection.Query(entityType, sqlCommand, paras, transaction: context.Transaction);
        }
    }
}
