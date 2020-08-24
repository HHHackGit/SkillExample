using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using NikeStore.Bootstraper;
using NikeStore.Factories;
using NikeStore.MainShowService;
using NikeStore.Resolvers;

namespace NikeStore.Modules
{
	sealed class ServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterType<MainViewFactory>()
				.As<IMainViewFactory>()
				.SingleInstance();

			builder
				.RegisterType<ViewResolver>()
				.As<IViewResolver>()
				.SingleInstance();

			builder
				.RegisterType<MainViewShowService>()
				.As<IMainViewShowService>()
				.SingleInstance();

			builder
				.RegisterType<AppBootstraper>()
				.As<IAppBootstraper>()
				.SingleInstance();

			builder
				.RegisterType<AppBootstraperBuilder>()
				.As<IAppBootstraperBuilder>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
