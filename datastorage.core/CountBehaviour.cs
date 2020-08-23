using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Datastorage.Framework;

using Support;

namespace Datastorage.Core
{
	internal sealed class CountBehaviour<T> : BehaviourBase<T>
		where T : class, IIdentifiable<long>
	{
		#region Data

		private readonly IDbContextFactory _dbContextFactory;

		#endregion

		#region .ctor

		public CountBehaviour(IDbContextFactory dbContextFactory)
		{
			Verify.Arguments.IsNotNull(dbContextFactory, nameof(dbContextFactory));

			_dbContextFactory = dbContextFactory;
		}

		#endregion

		#region ICountRepository implementation

		public async Task<bool> HasAnyRecords(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default)
		{
			using var context = await _dbContextFactory
				.CreateAsync()
				.ConfigureAwait(continueOnCapturedContext: false);

			var query = ApplyFilter(GetDbSet(context).AsNoTracking(), filter);

			return await query
				.AnyAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<int> GetRecordsCountAsync(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default)
		{
			using var context = await _dbContextFactory
				.CreateAsync()
				.ConfigureAwait(continueOnCapturedContext: false);

			var query = ApplyFilter(GetDbSet(context).AsNoTracking(), filter);

			return await query
				.CountAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		#endregion
	}
}
