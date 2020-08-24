using System;

using Autofac;

using NikeStore.Extensions.Autofac;
using NikeStore.Views;

namespace NikeStore.Modules
{
	sealed class ViewsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterType<MainWindowViewModel>()
				.SingleInstance()
				.AsSelf();
			builder
				.RegisterType<MainWindow>()
				.WithViewModel().OfType<MainWindowViewModel>()
				.AsSelf()
				.SingleInstance();

			builder
				.RegisterType<HeaderViewModel>()
				.SingleInstance()
				.AsSelf();
			builder
				.RegisterType<HeaderView>()
				.WithViewModel().OfType<HeaderViewModel>()
				.AsSelf()
				.SingleInstance();

			builder
				.RegisterType<BodyViewModel>()
				.SingleInstance()
				.AsSelf();
			builder
				.RegisterType<BodyView>()
				.WithViewModel().OfType<BodyViewModel>()
				.AsSelf()
				.SingleInstance();

			builder
				.RegisterType<StubView>()
				.AsSelf()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
