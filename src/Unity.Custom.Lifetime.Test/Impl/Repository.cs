using Unity.Custom.Lifetime.Test.Interfaces;

namespace Unity.Custom.Lifetime.Test.Impl;

public class Repository : IRepository
{
    private bool disposed;

    public bool IsDisposed()
    {
        return this.disposed;
    }

    public void Dispose()
    {
        this.disposed = true;
    }
}