using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Support;

using NikeStore.Factories;
using NikeStore.Splash;

namespace NikeStore.MainShowService
{
	sealed class MainViewShowService : IMainViewShowService
	{
		#region Data

		private readonly IMainViewFactory _mainViewFactory;

		private readonly ISplashScreenService _splashScreenService;

		private TaskCompletionSource<object> _mainViewShowTask;

		#endregion

		#region ctor

		public MainViewShowService(IMainViewFactory mainViewFactory)
		{
			Verify.Arguments.IsNotNull(mainViewFactory, nameof(mainViewFactory));

			_mainViewFactory     = mainViewFactory;
			_mainViewShowTask    = new TaskCompletionSource<object>();
		}

		#endregion

		#region IMainViewShowService

		public async Task ShowAsync(CancellationToken cancellationToken)
		{
			try
			{
				var mainView = _mainViewFactory.Create();

				mainView.Closed += (s, e) => { _mainViewShowTask.SetResult(null); };
				mainView.Loaded += OnMainViewLoaded;

				mainView.Show();

				await _mainViewShowTask.Task;
			}
			catch
			{
				//Log
			}
		}

		private async void OnMainViewLoaded(object sender, RoutedEventArgs e)
		{
			try
			{

			}
			catch
			{
				//Log
			}
		}

		#endregion
	}
}
