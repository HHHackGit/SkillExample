using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;

namespace NikeStore.Navigation
{
	public static class NavigationManager
	{
		#region Static

		private static Dictionary<string, NavigationService> _links;

		#endregion

		#region .ctor

		static NavigationManager()
		{
			_links = new Dictionary<string, NavigationService>();
		}

		#endregion

		#region Public Methods

		public static NavigationService GetNavigationService(string regionName)
		{
			return _links.SingleOrDefault(x => x.Key == regionName).Value;
		}

		public static void AddNavigationService(string regionName, NavigationService navigationService)
		{
			_links.Add(regionName, navigationService);
		}

		#endregion
	}
}
