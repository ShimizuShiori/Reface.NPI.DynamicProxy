using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite
{
    public interface ITask
    {
        string TaskName { get; }

        void DoTask(IUserDao userDao, Dictionary<string, object> context);
    }
}
