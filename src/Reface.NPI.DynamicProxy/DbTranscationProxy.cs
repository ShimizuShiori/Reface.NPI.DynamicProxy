using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.NPI.DynamicProxy
{
    public class DbTranscationProxy : IDbTransaction
    {
        private readonly IDbTransaction raw;

        public event EventHandler<EventArgs> Committed;
        public event EventHandler<EventArgs> Rollbacked;
        public event EventHandler<EventArgs> Disposing;

        public DbTranscationProxy(IDbTransaction raw)
        {
            this.raw = raw;
        }

        public IDbConnection Connection => raw.Connection;

        public IsolationLevel IsolationLevel => raw.IsolationLevel;

        public void Commit()
        {
            raw.Commit();
            Committed?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Disposing?.Invoke(this, EventArgs.Empty);
            raw.Dispose();
        }

        public void Rollback()
        {
            raw.Rollback();
            Rollbacked?.Invoke(this, EventArgs.Empty);
        }
    }
}
