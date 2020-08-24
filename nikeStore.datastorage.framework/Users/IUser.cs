using System;

using Datastorage.Framework;

namespace Nikestore.Datastorage.Framework.Users
{
	public interface IUser : IIdentifiable<long>
	{
		string Name { get; }

		string Surname { get; }

		GenderType Gender { get; }

		DateTimeOffset Birthday { get; }

		string Mail { get; }

		string Password { get; }
	}
}
