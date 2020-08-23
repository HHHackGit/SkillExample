using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Datastorage.Framework;

using Support;

namespace Datastorage.Core
{
	internal sealed class AddBehaviour<T> : BehaviourBase<T>, IAddRepository<T>
		where T : class, IIdentifiable<long>
	{
		#region Data

		private readonly IDbContextFactory _dbContextFactory;

		#endregion

		#region .ctor

		public AddBehaviour(IDbContextFactory dbContextFactory)
		{
			Verify.Arguments.IsNotNull(dbContextFactory, nameof(dbContextFactory));

			_dbContextFactory = dbContextFactory;
		}

		#endregion

		#region IAddRepository implementation

		public async Task<T> AddAsync(
			T @object,
			CancellationToken cancellationToken = default)
		{
			Verify.Arguments.IsNotNull(@object, nameof(@object));

			using var contex = await _dbContextFactory
				.CreateAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			var addedEntity = GetDbSet(contex).Add(@object);

			await contex
				.SaveChangesAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			return addedEntity;
		}

		public async Task<IReadOnlyList<T>> AddRangeAsync(
			IReadOnlyList<T> objects,
			CancellationToken cancellationToken = default)
		{
			using var contex = await _dbContextFactory
				.CreateAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			var dbSet = GetDbSet(contex);
			var addedEntities = new List<T>();

			foreach(var @object in objects)
			{
				addedEntities.Add(dbSet.Add(@object));
			}

			await contex
				.SaveChangesAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			return addedEntities.AsReadOnly();
		}

		#endregion
	}
}
