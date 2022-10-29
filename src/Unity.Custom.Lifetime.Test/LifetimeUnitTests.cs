using Microsoft.Practices.Unity;
using Unity.Custom.Lifetime.Test.Impl;
using Unity.Custom.Lifetime.Test.Interfaces;

namespace Unity.Custom.Lifetime.Test;

public class LifetimeUnitTests
{
    [Fact]
    public void LifetimeUnit_ContainerDefault()
    {
        IUnityContainer mainContainer = WireUp();

        IUnityContainer childContainer = mainContainer.CreateChildContainer();

        IService service = childContainer.Resolve<IService>();

        childContainer.Dispose();

        Assert.False(service.IsRepoDisposed());
    }

    [Fact]
    public void LifetimeUnit_AddExtension()
    {
        IUnityContainer mainContainer = WireUp();
        IRepository mainRepository = mainContainer.Resolve<IRepository>();
        ICache mainCache = mainContainer.Resolve<ICache>();

        IUnityContainer childContainer = mainContainer.CreateChildContainer().AddNewExtension<ForceHierarchicalLifetimeExtension>();
        IRepository childRepository = childContainer.Resolve<IRepository>();
        IService childService = childContainer.Resolve<IService>();
        ICache childCache = childContainer.Resolve<ICache>();

        Assert.True(ReferenceEquals(mainCache, childCache));

        // Resolve N-times the repository
        IRepository[] childRepositories = Enumerable.Range(0, 10)
            .Select(i => childContainer.Resolve<IRepository>())
            .ToArray();

        // The repository instance is always the same
        Assert.True(childRepositories.All(r => ReferenceEquals(r, childRepository)));

        // Resolve N-times the service
        IService[] childServices = Enumerable.Range(0, 10)
            .Select(i => childContainer.Resolve<IService>())
            .ToArray();

        // The service instance is always the same
        Assert.True(childServices.All(s => ReferenceEquals(s, childService)));

        childContainer.Dispose();

        // Assure caches are not disposed
        Assert.False(mainCache.IsDisposed());
        Assert.False(childCache.IsDisposed());

        // All services and repositories are disposed
        Assert.True(childRepositories.All(r => r.IsDisposed()));
        Assert.True(childServices.All(s => s.IsRepoDisposed()));

        // The main repository stays alive
        Assert.False(mainRepository.IsDisposed());

        // Any new repository stays also alive
        IRepository newRepository = mainContainer.Resolve<IRepository>();
        Assert.False(newRepository.IsDisposed());

        // And a new instance has been created
        Assert.False(ReferenceEquals(mainRepository, newRepository));

        ICache newCache = mainContainer.Resolve<ICache>();
        Assert.True(ReferenceEquals(mainCache, newCache));
        Assert.False(newCache.IsDisposed());
    }

    private static IUnityContainer WireUp()
    {
        IUnityContainer container = new UnityContainer();

        container.RegisterType<IRepository, Repository>();
        container.RegisterType<IService, Service>();
        container.RegisterType<ICache, Cache>(new ContainerControlledLifetimeManager());

        return container;
    }
}