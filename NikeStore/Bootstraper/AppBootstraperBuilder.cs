using System;
using System.Threading;
using System.Threading.Tasks;

using Support;

namespace NikeStore.Bootstraper
{
	sealed class AppBootstraperBuilder : IAppBootstraperBuilder
	{
		#region Properties

		private Func<CancellationToken, Task> _startFunc;

		private Func<CancellationToken, Task> _initializationFunc;

		private Func<CancellationToken, Task> _deinitializationFunc;

		#endregion

		#region IAppBootstraperBuilder implementation

		public IAppBootstraperBuilder WithStartStep(Func<CancellationToken, Task> startFunc)
		{
			Verify.Arguments.IsNotNull(startFunc, nameof(startFunc));

			_startFunc = startFunc;

			return this;
		}

		public IAppBootstraperBuilder WithInitializationStep(Func<CancellationToken, Task> initializationFunc)
		{
			Verify.Arguments.IsNotNull(initializationFunc, nameof(initializationFunc));

			_initializationFunc = initializationFunc;

			return this;
		}

		public IAppBootstraperBuilder WithDeinitializationStep(Func<CancellationToken, Task> deinitializationFunc)
		{
			Verify.Arguments.IsNotNull(deinitializationFunc, nameof(deinitializationFunc));

			_deinitializationFunc = deinitializationFunc;

			return this;
		}

		public IAppBootstraper Build()
		{
			return new AppBootstraper(_startFunc, _initializationFunc, _deinitializationFunc);
		}

		#endregion
	}
}
