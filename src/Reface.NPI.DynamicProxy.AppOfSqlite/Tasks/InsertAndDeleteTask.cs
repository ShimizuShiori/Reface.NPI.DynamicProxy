using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Tasks
{
    public class InsertAndDeleteTask : InsertAndDoSomeTask
    {
        public override string TaskName => "新增删除任务";

        protected override void DoSomeTask(IUserDao userDao, Dictionary<string, object> context, User insertedUser)
        {
            userDao.DeleteById(insertedUser.Id);
        }
    }
}
