using System;

namespace Datastorage.Framework
{
	public interface IIdentifiable<T>
	{
		T Id { get; }
	}
}
