using System.Collections.Generic;
using System.Data;
using static Dapper.SqlMapper;

namespace Reface.NPI.DynamicProxy
{
    public class DapperParameters : Dictionary<string, object>, IDynamicParameters
    {
        public void AddParameters(IDbCommand command, Identity identity)
        {
            foreach (var pair in this)
            {
                var cp = command.CreateParameter();
                cp.ParameterName = pair.Key;
                cp.Value = pair.Value;
                command.Parameters.Add(cp);
            }
        }
    }
}
