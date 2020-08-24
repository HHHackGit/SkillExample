using System;

using Autofac;

using Nikestore.Datastorage.Modules;

namespace NikeStore.Modules
{
	sealed class AppModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<DatastorageModule>();

			base.Load(builder);
		}
	}
}
