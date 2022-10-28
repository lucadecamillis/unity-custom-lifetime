using Unity.Custom.Lifetime.Test.Impl;
using Unity.Custom.Lifetime.Test.Interfaces;

namespace Unity.Custom.Lifetime.Test;

public class LifetimeUnitTests
{
    [Fact]
    public void LifetimeUnit_Container()
    {
        IUnityContainer mainContainer = WireUp();

        IUnityContainer childContainer = mainContainer.CreateChildContainer();

        IService service = childContainer.Resolve<IService>();

        childContainer.Dispose();

        Assert.True(service.IsRepoDisposed());
    }

    private IUnityContainer WireUp()
    {
        IUnityContainer container = new UnityContainer();

        container.RegisterType<IRepository, Repository>();
        container.RegisterType<IService, Service>();

        return container;
    }
}