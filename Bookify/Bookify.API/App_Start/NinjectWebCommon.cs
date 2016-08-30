using System;
using System.Configuration;
using System.Web;
using System.Web.Http;

using Bookify.API;
using Bookify.Common.Repositories;
using Bookify.DataAccess;
using Bookify.DataAccess.Repositories;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Newtonsoft.Json;

using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Bookify.API
{
    public static class NinjectWebCommon
    {
        private const string ConfigName = "UseAzure";
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            try
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings[ConfigName]))
                    kernel.Bind<IFileServerRepository>().To<AzureFileServerRepository>().InRequestScope();
                else
                    kernel.Bind<IFileServerRepository>().To<WindowsFileServerRepository>().InRequestScope();
            }
            catch (Exception e)
            {
                throw e;
            }
            kernel.Bind<BookifyContext>().ToSelf().InRequestScope();
            kernel.Bind<IAuthenticationRepository>().To<AuthenticationRepository>().InRequestScope();
            kernel.Bind<IBookFeedbackRepository>().To<BookFeedbackRepository>().InRequestScope();
            kernel.Bind<IBookRepository>().To<BookRepository>().InRequestScope();
            kernel.Bind<IPublisherRepository>().To<PublisherRepository>().InRequestScope();
            kernel.Bind<IPersonRepository>().To<PersonRepository>().InRequestScope();
            kernel.Bind<IPersonRoleRepository>().To<PersonRoleRepository>().InRequestScope();
            kernel.Bind<IPaymentInfoRepository>().To<PaymentInfoRepository>().InRequestScope();
            kernel.Bind<IGenreRepository>().To<GenreRepository>().InRequestScope();
            kernel.Bind<IBookOrderRepository>().To<BookOrderRepository>().InRequestScope();
            kernel.Bind<IBookHistoryRepository>().To<BookHistoryRepository>().InRequestScope();
            kernel.Bind<IAuthorRepository>().To<AuthorRepository>().InRequestScope();
            kernel.Bind<IAddressRepository>().To<AddressRepository>().InRequestScope();
            kernel.Bind<IBrewerRepository>().To<BrewerRepository>().InRequestScope();
        }
    }
}
