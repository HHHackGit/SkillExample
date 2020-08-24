using System;
using System.Threading;
using System.Threading.Tasks;

using Datastorage.Framework;

namespace Nikestore.Datastorage.Users
{
	public interface IUserRepository : IRepository<User, UserModification>
	{
		Task<User> AddAsync(
			string name,
			string surname,
			GenderType gender,
			DateTimeOffset birthday,
			string mail,
			string password,
			CancellationToken cancellationToken = default);
	}
}
