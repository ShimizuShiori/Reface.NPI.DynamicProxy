using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Daos
{
    public interface IUserDao : INpiDao<User>
    {
        User SelectById(string id);

        void Insert(User user);

        void DeleteByName(string name);
    }
}
