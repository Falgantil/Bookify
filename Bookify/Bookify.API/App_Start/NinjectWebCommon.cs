[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Bookify.API.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Bookify.API.App_Start.NinjectWebCommon), "Stop")]

namespace Bookify.API.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using DataAccess;
    using Core;
    using System.Web.Http;
    using Ninject.Web.WebApi;
    using Controllers;
    using DataAccess.Repositories;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
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
            kernel.Bind<BookifyContext>().To<BookifyContext>().InSingletonScope();
            kernel.Bind<IBookRepository>().To<BookRepository>().InRequestScope();
            kernel.Bind<IPublisherRepository>().To<PublisherRepository>().InRequestScope();
            kernel.Bind<IPersonRepository>().To<PersonRepository>().InRequestScope();
            kernel.Bind<IPersonRoleRepository>().To<PersonRoleRepository>().InRequestScope();
            kernel.Bind<IPaymentInfoRepository>().To<PaymentInfoRepository>().InRequestScope();
            kernel.Bind<IGenreRepository>().To<GenreRepository>().InRequestScope();
            kernel.Bind<IBookOrderRepository>().To<BookOrderRepository>().InRequestScope();
            kernel.Bind<IBookHistoryRepository>().To<BookHistoryRepository>().InRequestScope();
            kernel.Bind<IBookContentRepository>().To<BookContentRepository>().InRequestScope();
            kernel.Bind<IAuthorRepository>().To<AuthorRepository>().InRequestScope();
            kernel.Bind<IAddressRepository>().To<AddressRepository>().InRequestScope();
        }        
    }
}
