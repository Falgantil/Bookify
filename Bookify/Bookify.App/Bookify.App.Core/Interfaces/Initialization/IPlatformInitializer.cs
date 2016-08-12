using Ninject;

namespace Bookify.App.Core.Interfaces.Initialization
{
    /// <summary>
    /// The <see cref="IPlatformInitializer"/> interface. Inherit this on each platform to define what extra services needs to be registered on each platform.
    /// </summary>
    public interface IPlatformInitializer
    {
        /// <summary>
        /// Invoked by the bootstrapper before any core-registration.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        void BeforeInit(IKernel kernel);

        /// <summary>
        /// Invoked by the bootstrapper after all core-registration.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        void AfterInit(IKernel kernel);
    }
}