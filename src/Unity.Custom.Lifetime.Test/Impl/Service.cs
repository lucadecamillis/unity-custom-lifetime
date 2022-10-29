using Unity.Custom.Lifetime.Test.Interfaces;

namespace Unity.Custom.Lifetime.Test.Impl;

public class Service : IService
{
    readonly IRepository repository;
    readonly ICache cache;

    public Service(
        IRepository repository,
        ICache cache)
    {
        this.repository = repository;
        this.cache = cache;
    }

    public bool IsRepoDisposed()
    {
        return this.repository.IsDisposed();
    }

    public bool IsCacheDisposed()
    {
        return this.cache.IsDisposed();
    }
}