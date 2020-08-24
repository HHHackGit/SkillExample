using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Support;

using NikeStore.Navigation;
using NikeStore.Resolvers;

namespace NikeStore.Views
{
	public partial class MainWindowViewModel
	{
		#region Data

		private readonly IViewResolver _viewResolver;

		#endregion

		#region .ctor

		public MainWindowViewModel(IViewResolver viewResolver)
		{
			Verify.Arguments.IsNotNull(viewResolver, nameof(viewResolver));

			_viewResolver = viewResolver;
		}

		#endregion

		#region Private Methods

		private void LoadAsync()
		{
			try
			{
				NavigationManager
					.GetNavigationService(RegionsNames.HeaderRegionName)
					.Navigate(_viewResolver.Resolve<HeaderView>());

				NavigationManager
					.GetNavigationService(RegionsNames.MainRegionName)
					.Navigate(_viewResolver.Resolve<HeaderView>());
			}
			catch
			{
				//log
			}
		}

		private void UnloadAsync()
		{
			try
			{

			}
			catch
			{
				//log
			}
		}

		#endregion
	}
}
