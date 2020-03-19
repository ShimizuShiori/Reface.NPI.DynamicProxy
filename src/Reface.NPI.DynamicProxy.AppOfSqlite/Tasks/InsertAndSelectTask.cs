using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Tasks
{
    public class InsertAndSelectTask : InsertAndDoSomeTask
    {
        public override string TaskName => "新增查询任务";

        protected override void DoSomeTask(IUserDao userDao, Dictionary<string, object> context, User insertedUser)
        {
            User user = userDao.SelectById(insertedUser.Id);
            Checker.IsNotNull(user);
            Checker.Equals(insertedUser.Id, user.Id);
            Checker.Equals(insertedUser.Name, user.Name);
            Checker.Equals(insertedUser.LoginName, user.LoginName);
            Checker.Equals(insertedUser.Password, user.Password);
        }
    }
}
