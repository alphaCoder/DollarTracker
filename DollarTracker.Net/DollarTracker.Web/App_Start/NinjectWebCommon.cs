[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DollarTracker.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DollarTracker.Web.App_Start.NinjectWebCommon), "Stop")]

namespace DollarTracker.Web.App_Start
{
    using System;
    using System.Web;
	using System.Reflection;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

	using DollarTracker.Core.Infrastructure;
	using DollarTracker.Core.Repository;
	using DollarTracker.Core.Managers;

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
		/// Creates an instance of IKernal
		/// </summary>
		public static IKernel Kernel { get; private set; }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            Kernel = new StandardKernel();
            try
            {
                Kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                Kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(Kernel);
                return Kernel;
            }
            catch
            {
                Kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
			kernel.Load(Assembly.GetExecutingAssembly());
			kernel.Bind<IDbFactory>().To<DbFactory>().InRequestScope();
			kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
			kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
			kernel.Bind<IExpenseCategoryRepository>().To<ExpenseCategoryRepository>().InRequestScope();
			kernel.Bind<IExpenseCategoryManager>().To<ExpenseCategoryManager>().InRequestScope();
			kernel.Bind<IExpenseRepository>().To<ExpenseRepository>().InRequestScope();
			kernel.Bind<IExpenseManager>().To<ExpenseManager>().InRequestScope();
			kernel.Bind<IExpenseStoryRepository>().To<ExpenseStoryRepository>().InRequestScope();
			kernel.Bind<IExpenseStoryManager>().To<ExpenseStoryManager>().InRequestScope();
			kernel.Bind<IAppSettingManager>().To<AppSettingManager>().InRequestScope();
        }        
    }
}
