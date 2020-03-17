using Reface.NPI.Generators;
using System;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy
{
    public interface ISqlCommandQuerier
    {
        IEnumerable<object> Select(Type entityType, string sqlCommand, DapperParameters paras, DbConnectionContext context);
    }
}
