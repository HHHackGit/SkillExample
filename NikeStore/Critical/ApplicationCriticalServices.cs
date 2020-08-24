using System;

using Support;

using Autofac;

namespace NikeStore.Critical
{
	sealed class ApplicationCriticalServices : IApplicationCriticalServices
	{
		#region Data

		ApplicationCriticalServicesContext _context;

		#endregion

		#region Properties

		public IApplicationCriticalServicesContext Context { get; }

		public bool IsDisposed { get; private set; }

		#endregion

		#region .ctor

		public ApplicationCriticalServices()
		{
			_context = new ApplicationCriticalServicesContext();
		}

		#endregion

		#region Public Methods

		public ApplicationCriticalServices Include<T>(
			Func<IApplicationCriticalServicesContext, T> createFunc,
			Action<T, ContainerBuilder> registerAction,
			string serviceKey = default) where T : class
		{
			Verify.Arguments.IsNotNull(createFunc, nameof(createFunc));

			return Include(
				createFunc(_context),
				registerAction,
				serviceKey);
		}

		public ApplicationCriticalServices Include<T>(
			T service,
			Action<T, ContainerBuilder> registerAction,
			string serviceKey = default) where T : class
		{
			Verify.Arguments.IsNotNull(service, nameof(service));
			Verify.Arguments.IsNotNull(registerAction, nameof(registerAction));

			_context.AddService(
				service,
				registerAction,
				serviceKey);

			return this;
		}

		#endregion

		#region IApplicationCriticalServices implementation

		public void Register(ContainerBuilder containerBuilder)
		{
			Verify.Arguments.IsNotNull(containerBuilder, nameof(containerBuilder));

			_context.RegisterAllServices(containerBuilder);
		}

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{
			if(!IsDisposed)
			{
				//Dispose all services in context;

				IsDisposed = true;
			}
		}

		#endregion
	}
}
