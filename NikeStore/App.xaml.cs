using System;
using System.Windows;

using Autofac;

using NikeStore.SplashScreen;
using NikeStore.Critical;
using NikeStore.Bootstraper;
using NikeStore.MainShowService;

namespace NikeStore
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		#region .ctor

		public App()
		{
			ShutdownMode = ShutdownMode.OnExplicitShutdown;
		}

		#endregion

		#region Private Methods

		private ApplicationCriticalServices CreateCriticalServices()
		{
			return new ApplicationCriticalServices(); //Add critical services here
		}

		private IContainer CreateContainer(IApplicationCriticalServices criticalServices)
		{
			var builder = new ContainerBuilder();

			criticalServices?.Register(builder);

			builder.RegisterAssemblyModules(typeof(App).Assembly);

			return builder.Build();
		}

		private IAppBootstraper CreateBootstraper(IContainer container)
		{
			return container
				.Resolve<IAppBootstraperBuilder>()
				.WithInitializationStep(async x => { })
				.WithStartStep(async x =>
				{
					await container
						.Resolve<IMainViewShowService>()
						.ShowAsync(x);
				})
				.WithDeinitializationStep(async x => { })
				.Build();
		}

		#endregion

		#region Overrides

		protected async override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			using var criticalServices = CreateCriticalServices();
			var splashScreenService = criticalServices.Context.Get<ISplashScreenService>();

			if(splashScreenService != null)
			{
				splashScreenService.ShowSplashScreen();
			}

			try
			{
				using var container = CreateContainer(criticalServices);
				using var bootstraper = CreateBootstraper(container);

				await bootstraper.RunAsync();

				Shutdown();

				return;
			}
			finally
			{
				if(splashScreenService != null)
				{
					splashScreenService.ShowSplashScreen();
				}
			}
		}

		#endregion
	}
}
