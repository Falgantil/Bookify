﻿using System;
using System.Linq;

using Acr.UserDialogs;
using Bookify.App.Core.Interfaces;
using Bookify.App.Core.Interfaces.Initialization;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;
using Bookify.App.Sdk;
using Bookify.App.Sdk.Implementations;
using Bookify.App.Sdk.Interfaces;
using Ninject;
using Ninject.Parameters;
using PCLStorage;

namespace Bookify.App.Core.Initialization
{
    /// <summary>
    /// The shared Bootstrapper. Requires a <see cref="IPlatformInitializer"/> that will handle all platform-specific initialization.
    /// </summary>
    public sealed class Bootstrapper
    {
        /// <summary>
        /// The platform initializer
        /// </summary>
        private readonly IPlatformInitializer platformInitializer;

        /// <summary>
        /// The DI kernel
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        /// <param name="platformInitializer">The platform initializer.</param>
        private Bootstrapper(IPlatformInitializer platformInitializer)
        {
            this.platformInitializer = platformInitializer;
        }

        /// <summary>
        /// Creates an instance of <see cref="Bootstrapper" /> that automatically initializes the Bootstrapper and <see cref="platformInitializer" />.
        /// </summary>
        /// <param name="platformInitializer">The platform initializer.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <see cref="platformInitializer"/> is null.</exception>
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

        /// <summary>
        /// Resolves an instance of type <see cref="T"/> from the DI kernel, using any provided parameters.
        /// </summary>
        /// <typeparam name="T">Any type that requires constructor parameters to be resolved using types from the registered kernel.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T Resolve<T>(params Parameter[] parameters)
        {
            var args = parameters.Select(p => (IParameter)new ConstructorArgument(p.ParamName, p.Value));
            return this.kernel.Get<T>(args.ToArray());
        }

        /// <summary>
        /// Initializes this instance, as well as the registered PlatformInitializer.
        /// </summary>
        private void Initialize()
        {
            this.kernel = new StandardKernel();

            ApiConfig.Website = "https://bookifyapi.azurewebsites.net/";

            this.platformInitializer.BeforeInit(this.kernel);

            // Create all platform independent bindings here
            this.kernel.Bind<IUserDialogs>().ToMethod(c => UserDialogs.Instance);
            this.kernel.Bind<IFileSystem>().ToMethod(c => FileSystem.Current);
            this.kernel.Bind<IDelayService>().To<DelayService>().InSingletonScope();
            this.kernel.Bind<ICachingRegionFactory>().To<CachingRegionFactory>().InSingletonScope();

            this.kernel.Bind<IAuthenticationApi>().To<AuthenticationApi>().InSingletonScope();
            this.kernel.Bind<IBooksApi>().To<BooksApi>().InSingletonScope();
            this.kernel.Bind<IGenresApi>().To<GenresApi>().InSingletonScope();
            this.kernel.Bind<IPersonApi>().To<PersonApi>().InSingletonScope();
            this.kernel.Bind<IFeedbackApi>().To<FeedbackApi>().InSingletonScope();
            this.kernel.Bind<IFilesApi>().To<FilesApi>().InSingletonScope();

            this.kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InSingletonScope();
            this.kernel.Bind<IPersonService>().To<PersonService>().InSingletonScope();
            this.kernel.Bind<IShoppingCartService>().To<ShoppingCartService>().InSingletonScope();
            this.kernel.Bind<IBooksService>().To<BooksService>().InSingletonScope();
            this.kernel.Bind<IFeedbackService>().To<FeedbackService>().InSingletonScope();
            this.kernel.Bind<IGenreService>().To<GenreService>().InSingletonScope();

            this.kernel.Bind<IConfig>().To<Config>().InSingletonScope();

            this.platformInitializer.AfterInit(this.kernel);
        }
    }
}
