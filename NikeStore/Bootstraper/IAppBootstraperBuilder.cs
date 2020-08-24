using System;
using System.Threading;
using System.Threading.Tasks;

namespace NikeStore.Bootstraper
{
	public interface IAppBootstraperBuilder
	{
		IAppBootstraperBuilder WithInitializationStep(Func<CancellationToken, Task> initializationFunc);

		IAppBootstraperBuilder WithStartStep(Func<CancellationToken, Task> startFunc);

		IAppBootstraperBuilder WithDeinitializationStep(Func<CancellationToken, Task> deinitializationFunc);

		IAppBootstraper Build();
	}
}
