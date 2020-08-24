using System;
using System.Windows;

using Support;

using NikeStore.Resolvers;

namespace NikeStore.Factories
{
	sealed class MainViewFactory : IMainViewFactory
	{
		#region Data

		private readonly IViewResolver _viewResolver;

		#endregion

		#region .ctor

		public MainViewFactory(IViewResolver viewResolver)
		{
			Verify.Arguments.IsNotNull(viewResolver, nameof(viewResolver));

			_viewResolver = viewResolver;
		}

		#endregion

		#region IMainViewFactory implementation

		public Window Create()
		{
			return _viewResolver.Resolve<MainWindow>();
		}

		#endregion
	}
}
