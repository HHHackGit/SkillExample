using Datastorage.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nikestore.Datastorage.Framework.Users
{
	public interface IUserRepository : IRepository<IUser, UserModification>
	{

	}
}
