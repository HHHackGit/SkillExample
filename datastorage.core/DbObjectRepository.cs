using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Datastorage.Framework;

using Support;

namespace Datastorage.Core
{
	public abstract class DbObjectRepository<TObject, TModification> : IRepository<TObject, TModification>
		where TObject : class, IIdentifiable<long>
	{
		#region Data

		protected readonly IDbContextFactory _dbContextFactory;

		private readonly AddBehaviour<TObject> _addBehaviour;

		private readonly FetchBehaviour<TObject> _fetchBehaviour;

		private readonly ModifyBehaviour<TObject, TModification> _modifyBehaviour;

		private readonly RemoveBehaviour<TObject> _removeBehaviour;

		private readonly CountBehaviour<TObject> _countBehaviour;

		#endregion

		#region .ctor

		public DbObjectRepository(IDbContextFactory dbContextFactory)
		{
			Verify.Arguments.IsNotNull(dbContextFactory, nameof(dbContextFactory));

			_dbContextFactory = dbContextFactory;
			_addBehaviour     = new AddBehaviour<TObject>(dbContextFactory);
			_fetchBehaviour   = new FetchBehaviour<TObject>(dbContextFactory);
			_modifyBehaviour  = new ModifyBehaviour<TObject, TModification>(dbContextFactory, Modify);
			_removeBehaviour  = new RemoveBehaviour<TObject>(dbContextFactory);
			_countBehaviour   = new CountBehaviour<TObject>(dbContextFactory);
		}

		#endregion

		#region IAddRepository implementation

		public async Task<TObject> AddAsync(
			TObject @object, 
			CancellationToken cancellationToken = default)
		{
			return await _addBehaviour
				.AddAsync(
					@object: @object, 
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<IReadOnlyList<TObject>> AddRangeAsync(
			IReadOnlyList<TObject> objects, 
			CancellationToken cancellationToken = default)
		{
			return await _addBehaviour.AddRangeAsync(
					objects: objects,
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		#endregion

		#region IFetchRepository implementation

		public async Task<TObject> FetchRecordAsync(
			Expression<Func<TObject, bool>> filter, 
			CancellationToken cancellationToken = default, 
			params Expression<Func<TObject, object>>[] includes)
		{
			return await _fetchBehaviour.FetchRecordAsync(
					filter: filter,
					cancellationToken: cancellationToken,
					includes: includes)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<IReadOnlyList<TObject>> FetchRecordsAsync(
			Expression<Func<TObject, bool>> filter, 
			CancellationToken cancellationToken = default, 
			params Expression<Func<TObject, object>>[] includes)
		{
			return await _fetchBehaviour.FetchRecordsAsync(
					filter: filter,
					cancellationToken: cancellationToken,
					includes: includes)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		#endregion

		#region IModifyRepository implementation

		public async Task<IReadOnlyList<TObject>> ModifyAsync(
			Expression<Func<TObject, bool>> filter, 
			TModification modification, 
			CancellationToken cancellationToken = default)
		{
			return await _modifyBehaviour.ModifyAsync(
					filter: filter,
					modification: modification,
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<TObject> ModifyAsync(
			TObject @object, 
			TModification modification, 
			CancellationToken cancellationToken = default)
		{
			return await _modifyBehaviour.ModifyAsync(
				@object: @object,
				modification: modification,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
		}

		#endregion

		#region IRemoveRepository implementation

		public async Task RemoveAsync(
			Expression<Func<TObject, bool>> filter, 
			CancellationToken cancellationToken = default)
		{
			await _removeBehaviour
				.RemoveAsync(
					filter: filter,
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task RemoveAsync(
			TObject @object, 
			CancellationToken cancellationToken = default)
		{
			await _removeBehaviour
				.RemoveAsync(
					@object: @object,
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task RemoveRangeAsync(
			IReadOnlyList<TObject> objects,
			CancellationToken cancellationToken = default)
		{
			await _removeBehaviour
			.RemoveRangeAsync(
				objects: objects,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
		}


		#endregion

		#region ICountRepository implementation

		public async Task<bool> HasAnyRecords(
			Expression<Func<TObject, bool>> filter, 
			CancellationToken cancellationToken = default)
		{
			return await _countBehaviour
				.HasAnyRecords(
					filter: filter,
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		public async Task<int> GetRecordsCountAsync(
			Expression<Func<TObject, bool>> filter, 
			CancellationToken cancellationToken = default)
		{
			return await _countBehaviour
				.GetRecordsCountAsync(
					filter: filter,
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		#endregion

		#region Abstract Methods

		public abstract void Modify(TObject entity, TModification modification);

		#endregion
	}
}
