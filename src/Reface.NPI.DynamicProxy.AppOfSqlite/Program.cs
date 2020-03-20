using Reface.NPI.DynamicProxy.AppOfSqlite.Daos;
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
                context[Constant.CONTEXT_KEY_DB_CONTEXT] = ctx;

                userDao.Delete();

                foreach (var task in tasks)
                {
                    Console.Write("{0} : ", task.TaskName);
                    DebugLogger.Debug($"开始任务 : {task.TaskName}");
                    DateTime d1 = DateTime.Now;
                    try
                    {
                        task.DoTask(userDao, context);
                        DateTime d2 = DateTime.Now;
                        Console.WriteLine("SUCCESS\t{0} ms", (d2 - d1).Milliseconds);
                    }
                    catch (Exception ex)
                    {
                        DateTime d2 = DateTime.Now;
                        Console.WriteLine("Error\t{0} ms", (d2 - d1).Milliseconds);
                        Console.WriteLine(ex.ToString());
                        break;
                    }
                }
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
