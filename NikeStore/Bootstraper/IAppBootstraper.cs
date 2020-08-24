using System;
using System.Threading.Tasks;

namespace NikeStore.Bootstraper
{
	public interface IAppBootstraper : IDisposable
	{
		Task RunAsync();
	}
}
