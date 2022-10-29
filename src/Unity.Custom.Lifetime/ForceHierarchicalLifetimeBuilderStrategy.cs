using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Unity.Custom.Lifetime
{
    public class ForceHierarchicalLifetimeBuilderStrategy : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            IPolicyList lifetimePolicySource;

            var activeLifetime = context.PersistentPolicies.Get<ILifetimePolicy>(context.BuildKey, out lifetimePolicySource);
            if (!(activeLifetime is HierarchicalLifetimeManager))
            {
                var newLifetime = new HierarchicalLifetimeManager();
                context.PersistentPolicies.Set<ILifetimePolicy>(newLifetime, context.BuildKey);
                // Add to the lifetime container - we know this one is disposable
                context.Lifetime.Add(newLifetime);
            }
        }
    }
}