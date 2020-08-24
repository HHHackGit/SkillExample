using System;
using System.Threading;
using System.Threading.Tasks;

using Datastorage.Core;
using Datastorage.Framework;

namespace Nikestore.Datastorage.Users
{
	sealed class DbUserRepository : DbObjectRepository<User, UserModification>, IUserRepository
	{
		#region .ctor

		public DbUserRepository(IDbContextFactory dbContextFactory) 
			: base(dbContextFactory)
		{

		}

		#endregion

		#region Private Methods

		private User CreateUser(
			string name,
			string surname,
			GenderType gender,
			DateTimeOffset birthday,
			string mail,
			string password)
		{
			return new User
			{
				Name     = name,
				Surname  = surname,
				Gender   = gender,
				Birthday = birthday,
				Mail     = mail,
				Password = password,
			};
		}

		private bool IsEmptyModification(UserModification modification)
		{
			return modification.Name == null
				&& modification.Surname == null
				&& modification.Gender == null
				&& modification.Birthday == null
				&& modification.Password == null;
		}

		#endregion

		#region IUserRepository implementation

		public async Task<User> AddAsync(
			string name,
			string surname,
			GenderType gender,
			DateTimeOffset birthday,
			string mail,
			string password,
			CancellationToken cancellationToken = default)
		{
			return await AddAsync(
					CreateUser(
						name,
						surname,
						gender,
						birthday,
						mail,
						password),
					cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}

		#endregion

		#region Overrides

		public override void Modify(User user, UserModification modification)
		{
			if(IsEmptyModification(modification)) return;

			if(!string.IsNullOrWhiteSpace(modification.Name))
			{
				user.Name = modification.Name;
			}
			if(!string.IsNullOrWhiteSpace(modification.Surname))
			{
				user.Surname = modification.Surname;
			}
			if(modification.Gender.HasValue)
			{
				user.Gender = modification.Gender.Value;
			}
			if(modification.Birthday.HasValue)
			{
				user.Birthday = modification.Birthday.Value;
			}
			if(!string.IsNullOrWhiteSpace(modification.Password))
			{
				user.Password = modification.Password;
			}
		}

		#endregion
	}
}
