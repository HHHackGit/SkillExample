using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Datastorage.Framework
{
	public interface IAddRepository<T>
		where T : IIdentifiable<long>
	{
		Task<T> AddAsync(
			T @object, 
			CancellationToken cancellationToken = default);

		Task<IReadOnlyList<T>> AddRangeAsync(
			IReadOnlyList<T> objects,
			CancellationToken cancellationToken = default);
	}
}
