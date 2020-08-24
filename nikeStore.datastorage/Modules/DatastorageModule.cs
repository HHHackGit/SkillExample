using System;

using Autofac;

using Datastorage.Framework;

using Nikestore.Datastorage.Context;
using Nikestore.Datastorage.Users;

namespace Nikestore.Datastorage.Modules
{
	public sealed class DatastorageModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterType<DbUserRepository>()
				.As<IUserRepository>()
				.SingleInstance();

			builder
				.RegisterType<NikeStoreContextFactory>()
				.As<IDbContextFactory>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
