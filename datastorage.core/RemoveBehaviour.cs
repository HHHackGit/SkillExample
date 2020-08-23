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
	internal sealed class RemoveBehaviour<T> : BehaviourBase<T>
		where T : class, IIdentifiable<long>
	{
		#region Data

		private readonly IDbContextFactory _dbContextFactory;

		#endregion

		#region .ctor

		public RemoveBehaviour(IDbContextFactory dbContextFactory)
		{
			Verify.Arguments.IsNotNull(dbContextFactory, nameof(dbContextFactory));

			_dbContextFactory = dbContextFactory;
		}

		#endregion

		#region IRemoveRepository implementation

		public async Task RemoveAsync(
			T @object,
			CancellationToken cancellationToken = default)
		{
			using var context = await _dbContextFactory
				.CreateAsync()
				.ConfigureAwait(continueOnCapturedContext: false);

			GetDbSet(context).Remove(@object);

			await context
				.SaveChangesAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task RemoveRangeAsync(
			IReadOnlyList<T> objects,
			CancellationToken cancellationToken = default)
		{
			using var context = await _dbContextFactory
				.CreateAsync()
				.ConfigureAwait(continueOnCapturedContext: false);

			var dbSet = GetDbSet(context);

			foreach(var @object in objects)
			{
				dbSet.Remove(@object);
			}

			await context
				.SaveChangesAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task RemoveAsync(
			Expression<Func<T, bool>> filter,
			CancellationToken cancellationToken = default)
		{
			using var context = await _dbContextFactory
				.CreateAsync()
				.ConfigureAwait(continueOnCapturedContext: false);

			var dbSet = GetDbSet(context);
			var entities = await ApplyFilter(dbSet, filter)
				.ToListAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			entities.ForEach(x => dbSet.Remove(x));

			await context
				.SaveChangesAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		#endregion
	}
}
