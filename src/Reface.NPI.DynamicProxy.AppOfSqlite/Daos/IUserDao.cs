using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System.Collections.Generic;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Daos
{
    public interface IUserDao : INpiDao<User>
    {
        User SelectById(string id);

        void Insert(User user);

        void DeleteByName(string name);

        void DeleteById(string id);

        void UpdateLoginnameById(string loginName, string id);

        void Delete();

        IList<User> SelectOrderbyIdDesc();

        IList<User> GetOrderbyId();

        IList<User> SelectByIdIn(string[] idList);
    }
}
