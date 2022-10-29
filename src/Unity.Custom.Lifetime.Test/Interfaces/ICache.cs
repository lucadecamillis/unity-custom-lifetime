namespace Unity.Custom.Lifetime.Test.Interfaces;

public interface ICache : IDisposable
{
    bool IsDisposed();
}