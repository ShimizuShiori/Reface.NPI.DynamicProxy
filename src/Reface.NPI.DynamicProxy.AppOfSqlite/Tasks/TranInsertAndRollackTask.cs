using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Tasks
{
    public class TranInsertAndRollackTask : ITask
    {
        public string TaskName => "写入并回滚任务";

        public void DoTask(IUserDao userDao, Dictionary<string, object> context)
        {
            DbConnectionContext ctx = (DbConnectionContext)context[Constant.CONTEXT_KEY_DB_CONTEXT];
            User user = User.New();
            ctx.BeginTran();
            userDao.Insert(user);
            ctx.Rollback();

            User user2 = userDao.SelectById(user.Id);
            Checker.IsNull(user2);
        }
    }
}
