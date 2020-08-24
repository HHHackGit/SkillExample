using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

using Datastorage.Framework;

namespace Nikestore.Datastorage.Context
{
	sealed class NikeStoreContextFactory : IDbContextFactory
	{
		public async Task<DbContext> CreateAsync(
			CancellationToken cancellationToken = default)
		{
			return new NikeStoreContext();
		}
	}
}
