[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(QuizGame.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(QuizGame.App_Start.NinjectWebCommon), "Stop")]

namespace QuizGame.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using QuizGame.Data;
    using QuizGame.Repo;
    using QuizGame.Service;
    using QuizGame.Service.BankAccount;

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
                System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
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
            kernel.Bind<EZSMdbEntitie>().ToSelf().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind(typeof(ILoginService)).To(typeof(LoginService));
            kernel.Bind(typeof(IActivityLogService)).To(typeof(ActivityLogService));
            kernel.Bind(typeof(IRoleService)).To(typeof(RoleService));
            kernel.Bind(typeof(IUserService)).To(typeof(UserService));
            kernel.Bind(typeof(IMenuService)).To(typeof(MenuService));
            kernel.Bind(typeof(IBlogService)).To(typeof(BlogService));
            kernel.Bind(typeof(IEventService)).To(typeof(EventService));
            kernel.Bind(typeof(IBankAccountService)).To(typeof(BankAccountService));
        }

        public class NinjectDependencyResolver : IDependencyResolver
        {
            private readonly IKernel kernel;

            public NinjectDependencyResolver(IKernel kernel)
            {
                this.kernel = kernel;
            }

            public object GetService(Type serviceType)
            {
                return this.kernel.TryGet(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                try
                {
                    return this.kernel.GetAll(serviceType);
                }
                catch (Exception)
                {
                    return new List<object>();
                }
            }
        }
    }
}