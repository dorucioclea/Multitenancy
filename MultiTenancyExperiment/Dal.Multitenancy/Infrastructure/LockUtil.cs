using System;
using System.Threading;

namespace MultiTenancyExperiment.Dal.Multitenancy.Infrastructure
{
    class LockUtil : IDisposable
    {
        private readonly object _lockObj;

        public LockUtil(object lockObj) : this(lockObj, TimeSpan.FromSeconds(5)) { }

        public LockUtil(object lockObj, TimeSpan timeout)
        {
            this._lockObj = lockObj;
            if (!Monitor.TryEnter(this._lockObj, timeout))
                throw new TimeoutException();
        }

        public void Dispose()
        {
            Monitor.Exit(_lockObj);
        }
    }
}