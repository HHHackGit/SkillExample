using System;
using System.Data.Entity;

namespace Nikestore.Datastorage.Context
{
	sealed class NikeStoreContext : DbContext
	{
		#region .ctor

		public NikeStoreContext() : base("DbConnectionString")
		{

		}

		#endregion
	}
}
