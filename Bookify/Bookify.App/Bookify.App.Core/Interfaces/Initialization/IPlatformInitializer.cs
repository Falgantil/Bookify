using Ninject;

namespace Bookify.App.Core.Interfaces.Initialization
{
    public interface IPlatformInitializer
    {
        void BeforeInit(IKernel kernel);

        void AfterInit(IKernel kernel);
    }
}