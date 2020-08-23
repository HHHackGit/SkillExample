using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Datastorage.Framework
{
	public interface IFetchRepository<T>
		where T : IIdentifiable<long>
	{
		Task<IReadOnlyList<T>> FetchRecordsAsync(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default,
			params Expression<Func<T, object>>[] includes);

		Task<T> FetchRecordAsync(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default,
			params Expression<Func<T, object>>[] includes);
	}
}
