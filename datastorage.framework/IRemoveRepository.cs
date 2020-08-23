using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Datastorage.Framework
{
	public interface IRemoveRepository<T>
		where T : IIdentifiable<long>
	{
		Task RemoveAsync(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default);
		Task RemoveAsync(
			T @object, 
			CancellationToken cancellationToken = default);

		Task RemoveRangeAsync(
			IReadOnlyList<T> objects,
			CancellationToken cancellationToken = default);
	}
}
