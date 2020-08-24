using System;
using System.Threading;
using System.Threading.Tasks;

namespace NikeStore.MainShowService
{
	public interface IMainViewShowService
	{
		Task ShowAsync(CancellationToken cancellationToken);
	}
}
