using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Unity.Custom.Lifetime
{
    public class ForceHierarchicalLifetimeExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.AddNew<ForceHierarchicalLifetimeBuilderStrategy>(UnityBuildStage.Lifetime);
        }
    }
}