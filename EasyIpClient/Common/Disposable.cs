using System;

namespace EasyIpClient.Common
{
    public abstract class Disposable : IDisposable
    {
        // Flag: Has Dispose already been called?
        private bool _disposed;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;
        }

        ~Disposable()
        {
            Dispose(false);
        }
    }
}
