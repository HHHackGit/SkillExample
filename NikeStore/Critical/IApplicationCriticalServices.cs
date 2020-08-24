using System;

using Autofac;

namespace NikeStore.Critical
{
	public interface IApplicationCriticalServices : IDisposable
	{
		IApplicationCriticalServicesContext Context { get; }

		void Register(ContainerBuilder containerBuilder);
	}
}
