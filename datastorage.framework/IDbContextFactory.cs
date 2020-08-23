using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Datastorage.Framework
{
	public interface IDbContextFactory
	{
		Task<DbContext> CreateAsync(CancellationToken cancellationToken = default);
	}
}
