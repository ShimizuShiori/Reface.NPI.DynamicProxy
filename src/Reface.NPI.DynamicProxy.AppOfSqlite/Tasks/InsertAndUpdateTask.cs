using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Tasks
{
    public class InsertAndUpdateTask : InsertAndDoSomeTask
    {
        public override string TaskName => "新增更新任务";

        protected override void DoSomeTask(IUserDao userDao, Dictionary<string, object> context, User insertedUser)
        {
            userDao.UpdateLoginnameById("LoginName2", insertedUser.Id);
            var user = userDao.SelectById(insertedUser.Id);
            Checker.Equals("LoginName2", user.LoginName);
            Checker.Equals(insertedUser.Id, user.Id);
            Checker.Equals(insertedUser.Name, user.Name);
            Checker.Equals(insertedUser.Password, user.Password);
        }
    }
}
