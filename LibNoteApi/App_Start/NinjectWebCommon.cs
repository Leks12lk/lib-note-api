using LibNoteApi.Infrastructure;
using LibNoteApi.Repositories;
using LibNoteApi.Repositories.Interfaces;
using LibNoteApi.Services;
using LibNoteApi.Services.Interfaces;
using Ninject.Web.Common.WebHost;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LibNoteApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LibNoteApi.App_Start.NinjectWebCommon), "Stop")]


namespace LibNoteApi.App_Start
{
	using System;
	using System.Web;

	using Microsoft.Web.Infrastructure.DynamicModuleHelper;

	using Ninject;
	using Ninject.Web.Common;
	using System.Web.Http;


	public static class NinjectWebCommon
	{
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
		/// Creates the kernel that will manage an application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

			RegisterServices(kernel);

			// Install our Ninject-based IDependencyResolver into the Web API config
			GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

			return kernel;
		}

		/// <summary>
		/// Bind service to interface
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
		{
			kernel.Bind<INotificationRepository>().To<NotificationRepository>();
			kernel.Bind<INotificationService>().To<NotificationService>();
		}
	}
}
