using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Tasks
{
    public class InTask : ITask
    {
        public string TaskName => "In 任务";

        public void DoTask(IUserDao userDao, Dictionary<string, object> context)
        {
            userDao.Delete();
            userDao.Insert(new Entities.User() { Id = "1", Name = "", LoginName = "", Password = "" });
            userDao.Insert(new Entities.User() { Id = "2", Name = "", LoginName = "", Password = "" });
            userDao.Insert(new Entities.User() { Id = "3", Name = "", LoginName = "", Password = "" });
            userDao.Insert(new Entities.User() { Id = "4", Name = "", LoginName = "", Password = "" });
            IList<User> users = userDao.SelectByIdIn(new string[] { "1", "2", "3" });
            Checker.Equals(3, users.Count);
        }
    }
}
