using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;

namespace Reface.NPI.DynamicProxy.AppOfSqlite
{
    class Program
    {
        static void Main(string[] args)
        {

            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "data\\app.db";
            using (SQLiteConnection conn = new SQLiteConnection(connectionStringBuilder.ToString()))
            {
                conn.Open();
                DbConnectionContext ctx = new DbConnectionContext(conn);
                INPIImplementer implementer = new NPIImplementer(ctx);

                IUserDao userDao = implementer.Implement<IUserDao>();
                User user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    LoginName = "admin",
                    Name = "shiori",
                    Password = "888888"
                };
                userDao.Insert(user);
                userDao.DeleteByName(user.Name);
            }
            Console.ReadLine();
        }
    }
}
