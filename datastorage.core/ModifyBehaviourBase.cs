using System;

using Datastorage.Framework;

namespace Datastorage.Core
{
	abstract class ModifyBehaviourBase<TObject, TModification> : BehaviourBase<TObject>
		where TObject : class, IIdentifiable<long>
	{
		#region Abstract Methods

		protected abstract void Modify(
			TObject entity,
			TModification modification);

		#endregion
	}
}
