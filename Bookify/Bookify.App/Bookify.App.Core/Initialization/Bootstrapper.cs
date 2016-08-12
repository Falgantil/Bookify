using System;
using System.Linq;

using Acr.UserDialogs;

using Bookify.App.Core.Interfaces.Initialization;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;

using Ninject;
using Ninject.Parameters;

namespace Bookify.App.Core.Initialization
{
    public sealed class Bootstrapper
    {
        private readonly IPlatformInitializer platformInitializer;

        private IKernel kernel;

        private Bootstrapper(IPlatformInitializer platformInitializer)
        {
            this.platformInitializer = platformInitializer;
        }

        public static Bootstrapper Initialize(IPlatformInitializer platformInitializer)
        {
            if (platformInitializer == null)
            {
                throw new ArgumentNullException(nameof(platformInitializer));
            }
            var bootstrapper = new Bootstrapper(platformInitializer);
            bootstrapper.Initialize();
            return bootstrapper;
        }

        private void Initialize()
        {
            this.kernel = new StandardKernel();

            this.platformInitializer.BeforeInit(this.kernel);

            // Create all platform independent bindings here
            this.kernel.Bind<IUserDialogs>().ToMethod(c => UserDialogs.Instance);

            this.kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InSingletonScope();
            this.kernel.Bind<IBookService>().To<BookService>().InSingletonScope();
            this.kernel.Bind<IReviewService>().To<ReviewService>().InSingletonScope();

            this.platformInitializer.AfterInit(this.kernel);
        }

        public T Resolve<T>(params Parameter[] parameters)
        {
            var args = parameters.Select(p => new ConstructorArgument(p.PropertyName, p.Value));
            return this.kernel.Get<T>(args.ToArray());
        }
    }
}
