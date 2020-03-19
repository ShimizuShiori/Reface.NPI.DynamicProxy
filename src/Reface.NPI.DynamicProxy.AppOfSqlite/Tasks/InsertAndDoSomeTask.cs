using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Tasks
{
    public abstract class InsertAndDoSomeTask : ITask
    {
        public abstract string TaskName { get; }

        public void DoTask(IUserDao userDao, Dictionary<string, object> context)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Name",
                LoginName = "LoginName",
                Password = "Password"
            };
            userDao.Insert(user);
            DoSomeTask(userDao, context, user);
        }

        protected abstract void DoSomeTask(IUserDao userDao, Dictionary<string, object> context, User insertedUser);
    }
}
