namespace Unity.Custom.Lifetime.Test.Interfaces;

public interface IRepository : IDisposable
{
    bool IsDisposed();
}