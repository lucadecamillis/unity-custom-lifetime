namespace Unity.Custom.Lifetime.Test.Interfaces;

public interface IService
{
    bool IsRepoDisposed();

    bool IsCacheDisposed();
}