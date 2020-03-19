using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Tasks
{
    public class OrderbyTask : ITask
    {
        public string TaskName => "排序任务";

        public void DoTask(IUserDao userDao, Dictionary<string, object> context)
        {
            userDao.Delete();
            userDao.Insert(new Entities.User() { Id = "1", Name = "", LoginName = "", Password = "" });
            userDao.Insert(new Entities.User() { Id = "2", Name = "", LoginName = "", Password = "" });
            userDao.Insert(new Entities.User() { Id = "3", Name = "", LoginName = "", Password = "" });
            userDao.Insert(new Entities.User() { Id = "4", Name = "", LoginName = "", Password = "" });
            IList<User> users = userDao.SelectOrderbyIdDesc();
            Checker.Equals(4, users.Count);
            Checker.Equals("4", users[0].Id);
            Checker.Equals("3", users[1].Id);
            Checker.Equals("2", users[2].Id);
            Checker.Equals("1", users[3].Id);


            userDao.Delete();
            userDao.Insert(new Entities.User() { Id = "1", Name = "", LoginName = "", Password = "" });
            userDao.Insert(new Entities.User() { Id = "2", Name = "", LoginName = "", Password = "" });
            userDao.Insert(new Entities.User() { Id = "3", Name = "", LoginName = "", Password = "" });
            userDao.Insert(new Entities.User() { Id = "4", Name = "", LoginName = "", Password = "" });
            users = userDao.GetOrderbyId();
            Checker.Equals(4, users.Count);
            Checker.Equals("1", users[0].Id);
            Checker.Equals("2", users[1].Id);
            Checker.Equals("3", users[2].Id);
            Checker.Equals("4", users[3].Id);
        }
    }
}
