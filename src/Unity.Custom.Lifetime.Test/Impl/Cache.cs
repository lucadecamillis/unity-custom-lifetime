using Unity.Custom.Lifetime.Test.Interfaces;

namespace Unity.Custom.Lifetime.Test.Impl;

public class Cache : ICache
{
    private bool disposed;

    public Cache()
    {
        
    }

    public bool IsDisposed()
    {
        return this.disposed;
    }

    public void Dispose()
    {
        this.disposed = true;
    }
}