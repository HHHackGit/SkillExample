using System;
using System.Threading;
using System.Threading.Tasks;

namespace NikeStore.Bootstraper
{
	sealed class AppBootstraper : IAppBootstraper
	{
		#region Data

		private readonly Func<CancellationToken, Task> _startFunc;

		private readonly Func<CancellationToken, Task> _initializationFunc;

		private readonly Func<CancellationToken, Task> _deinitializationFunc;


		#endregion

		#region Properties

		/// <summary>Возвращает флаг запущенного приложения.</summary>
		private bool IsRunning { get; set; }

		public bool IsDisposed { get; private set; }

		#endregion

		#region .ctor

		public AppBootstraper(
			Func<CancellationToken, Task> startFunc,
			Func<CancellationToken, Task> initializationFunc,
			Func<CancellationToken, Task> deinitializationFunc)
		{
			_startFunc            = startFunc;
			_initializationFunc   = initializationFunc;
			_deinitializationFunc = deinitializationFunc;
		}

		#endregion

		#region IAppBootstraper implementation

		public async Task RunAsync()
		{
			if(IsDisposed)
			{
				throw new ObjectDisposedException("AppBootstraper disposed.");
			}
			if(IsRunning)
			{
				throw new InvalidOperationException("Application is already running.");
			}

			IsRunning = true;

			var isServicesInitialized = false;

			try
			{
				if(_initializationFunc != null)
				{
					await _initializationFunc(CancellationToken.None);

					isServicesInitialized = true;
				}

				if(_startFunc != null)
				{
					await _startFunc(CancellationToken.None);
				}
			}
			finally
			{
				if(isServicesInitialized && _deinitializationFunc != null)
				{
					await _deinitializationFunc(CancellationToken.None);
				}

				IsRunning = false;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{
			if(!IsDisposed)
			{
				IsDisposed = true;
			}
		}

		#endregion
	}
}
