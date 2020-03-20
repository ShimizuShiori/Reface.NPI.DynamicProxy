using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Tasks
{
    public class TranInsertAndCommitTask : ITask
    {
        public string TaskName => "事务提交任务";

        public void DoTask(IUserDao userDao, Dictionary<string, object> context)
        {
            DbConnectionContext ctx = (DbConnectionContext)context[Constant.CONTEXT_KEY_DB_CONTEXT];

            User user = User.New();
            using (var tran = ctx.DbConnection.BeginTransaction())
            {
                ctx.Transaction = tran;
                userDao.Insert(user);
                tran.Commit();
                ctx.Transaction = null;
            }

            User user2 = userDao.SelectById(user.Id);
            Checker.IsNotNull(user2);
            Checker.Equals(user.Id, user2.Id);
            Checker.Equals(user.Name, user2.Name);
            Checker.Equals(user.LoginName, user2.LoginName);
            Checker.Equals(user.Password, user2.Password);
        }
    }
}
