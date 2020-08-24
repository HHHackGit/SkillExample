using System;

namespace Datastorage.Framework
{
	public interface IRepository<TObject, TModification> : IAddRepository<TObject>, IFetchRepository<TObject>, IModifyRepository<TObject, TModification>, IRemoveRepository<TObject>, ICountRepository<TObject>
		where TObject : class, IIdentifiable<long>
	{

	}
}
