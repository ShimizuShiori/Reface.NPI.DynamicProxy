using Reface.NPI.Generators;

namespace Reface.NPI.DynamicProxy
{
    public interface ISqlCommandExecutor
    {
        int Execute(string sqlCommand, DapperParameters paras, DbConnectionContext context);
    }
}
