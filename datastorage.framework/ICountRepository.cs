using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Datastorage.Framework
{
	public interface ICountRepository<T>
		where T : IIdentifiable<long>
	{
		Task<bool> HasAnyRecords(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default);

		Task<int> GetRecordsCountAsync(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default);
	}
}
