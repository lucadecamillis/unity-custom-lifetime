using Unity.Custom.Lifetime.Test.Interfaces;

namespace Unity.Custom.Lifetime.Test.Impl;

public class Service : IService
{
    readonly IRepository repository;

    public Service(IRepository repository)
    {
        this.repository = repository;
    }

    public bool IsRepoDisposed()
    {
        return this.repository.IsDisposed();
    }
}