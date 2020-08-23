using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Datastorage.Framework;

using Support;

namespace Datastorage.Core
{
	internal sealed class FetchBehaviour<T> : BehaviourBase<T>, IFetchRepository<T>
		where T: class, IIdentifiable<long>
	{
		#region Data

		private readonly IDbContextFactory _dbContextFactory;

		#endregion

		#region .ctor

		public FetchBehaviour(IDbContextFactory dbContextFactory)
		{
			Verify.Arguments.IsNotNull(dbContextFactory, nameof(dbContextFactory));

			_dbContextFactory = dbContextFactory;
		}

		#endregion

		#region IFetchRepository implementation

		public async Task<IReadOnlyList<T>> FetchRecordsAsync(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default,
			params Expression<Func<T, object>>[] includes)
		{
			using var context = await _dbContextFactory
				.CreateAsync()
				.ConfigureAwait(continueOnCapturedContext: false);

			var query = GetDbSet(context).AsNoTracking();
			query = ApplyIncludes(query, includes);
			query = ApplyFilter(query, filter);

			return await query
				.ToListAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<T> FetchRecordAsync(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default,
			params Expression<Func<T, object>>[] includes)
		{
			using var context = await _dbContextFactory
				.CreateAsync()
				.ConfigureAwait(continueOnCapturedContext: false);

			var query = GetDbSet(context).AsNoTracking();
			query = ApplyIncludes(query, includes);
			query = ApplyFilter(query, filter);

			return await query
				.FirstOrDefaultAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		#endregion
	}
}
