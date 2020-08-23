using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Datastorage.Framework;

using Support;

namespace Datastorage.Core
{
	internal sealed class ModifyBehaviour<TObject,TModification> : ModifyBehaviourBase<TObject, TModification>, IModifyRepository<TObject, TModification>
		where TObject : class, IIdentifiable<long>
	{
		#region Data

		private readonly IDbContextFactory _dbContextFactory;

		private readonly Action<TObject, TModification> _modifyFunc;

		#endregion

		#region .ctor

		public ModifyBehaviour(
			IDbContextFactory dbContextFactory,
			Action<TObject, TModification> modifyFunc)
		{
			Verify.Arguments.IsNotNull(dbContextFactory, nameof(dbContextFactory));
			Verify.Arguments.IsNotNull(modifyFunc, nameof(modifyFunc));

			_dbContextFactory = dbContextFactory;
			_modifyFunc       = modifyFunc;
		}

		#endregion

		#region IModifyRepository implementation

		public async Task<IReadOnlyList<TObject>> ModifyAsync(
			Expression<Func<TObject, bool>> filter,
			TModification modification,
			CancellationToken cancellationToken = default)
		{
			using var context = await _dbContextFactory
				.CreateAsync()
				.ConfigureAwait(continueOnCapturedContext: false);

			var entities = await ApplyFilter(GetDbSet(context), filter)
				.ToListAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			entities.ForEach(x => Modify(x, modification));

			await context
				.SaveChangesAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			return entities.AsReadOnly();
		}

		public async Task<TObject> ModifyAsync(
			TObject @object,
			TModification modification,
			CancellationToken cancellationToken = default)
		{
			return (await ModifyAsync(
					filter: x => x.Id == @object.Id,
					modification: modification,
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false))
				.FirstOrDefault();
		}

		protected override void Modify(
			TObject entity, 
			TModification modification)
		{
			_modifyFunc.Invoke(entity, modification);
		}

		#endregion
	}
}
