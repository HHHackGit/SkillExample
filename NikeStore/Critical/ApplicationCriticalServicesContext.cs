using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

namespace NikeStore.Critical
{
	sealed class ApplicationCriticalServicesContext : IApplicationCriticalServicesContext
	{
		#region Helpers

		private abstract class ServiceInfo
		{
			#region Properties

			public abstract string ServiceKey { get; }

			public abstract object Service { get; }

			#endregion

			#region Abstract Methdos

			public abstract void Register(ContainerBuilder containerBuilder);

			#endregion
		}

		private class ServiceInfo<T> : ServiceInfo
		{
			#region Properties

			public override object Service { get; }

			public override string ServiceKey { get; }

			public Action<T, ContainerBuilder> RegisterAction { get; }

			#endregion

			#region .ctor

			public ServiceInfo(
				T service,
				Action<T, ContainerBuilder> registerAction,
				string serviceKey)
			{
				Service = service;
				RegisterAction = registerAction;
				ServiceKey = serviceKey;
			}

			#endregion

			#region Overrides

			public override void Register(ContainerBuilder containerBuilder)
			{
				RegisterAction((T)Service, containerBuilder);
			}

			#endregion
		}

		#endregion

		#region Data

		private readonly List<ServiceInfo> _services;

		#endregion

		#region .ctor

		public ApplicationCriticalServicesContext()
		{
			_services = new List<ServiceInfo>();
		}

		#endregion

		#region Public Methods

		public void AddService<T>(
			T service,
			Action<T, ContainerBuilder> registerAction,
			string serviceKey = default)
		{
			var serviceInfo = new ServiceInfo<T>(
				service,
				registerAction,
				serviceKey);

			_services.Add(serviceInfo);
		}

		public void RegisterAllServices(ContainerBuilder containerBuilder)
		{
			foreach(var service in _services)
			{
				service.Register(containerBuilder);
			}
		}

		#endregion

		#region IApplicationCriticalServicesContext implementation

		public T Get<T>(string serviceKey = null) where T : class
		{
			return !string.IsNullOrWhiteSpace(serviceKey)
				? _services
					.SingleOrDefault(s => s.ServiceKey == serviceKey).Service as T
				: _services
					.Select(s => s.Service)
					.OfType<T>()
					.SingleOrDefault();
		}

		#endregion
	}
}
