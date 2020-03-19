using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
using Reface.NPI.DynamicProxy.AppOfSqlite.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Reface.NPI.DynamicProxy.AppOfSqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ITask> tasks = new List<ITask>();
            foreach (var type in typeof(ITask).Assembly.GetTypes())
            {
                if (!typeof(ITask).IsAssignableFrom(type))
                    continue;

                if (type.IsAbstract) continue;
                if (type.IsInterface) continue;

                tasks.Add((ITask)Activator.CreateInstance(type));
            }


            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder();
            connectionStringBuilder.DataSource = ".\\data\\app.db";
            using (SQLiteConnection conn = new SQLiteConnection(connectionStringBuilder.ToString()))
            {
                conn.Open();
                DbConnectionContext ctx = new DbConnectionContext(conn);
                INPIImplementer implementer = new NPIImplementer(ctx);

                IUserDao userDao = implementer.Implement<IUserDao>();

                Dictionary<string, object> context = new Dictionary<string, object>();

                userDao.Delete();


                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from t_user where id in @ids";
                    var ps = cmd.CreateParameter();
                    ps.ParameterName = "ids";
                    ps.Value = new string[] { "1", "2" };
                    cmd.Parameters.Add(ps);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString());
                        }
                    }
                }


                //foreach (var task in tasks)
                //{
                //    Console.Write("{0} : ", task.TaskName);
                //    try
                //    {
                //        task.DoTask(userDao, context);
                //        Console.WriteLine("SUCCESS");
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine("ERROR");
                //        Console.WriteLine(ex.ToString());
                //        break;
                //    }
                //}
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
