using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Datastorage.Framework
{
	public interface IModifyRepository<TObject, TModification>
		where TObject : IIdentifiable<long>
	{
		Task<IReadOnlyList<TObject>> ModifyAsync(
			Expression<Func<TObject, bool>> filter,
			TModification modification,
			CancellationToken cancellationToken = default);

		Task<TObject> ModifyAsync(
			TObject @object,
			TModification modification,
			CancellationToken cancellationToken = default);
	}
}
