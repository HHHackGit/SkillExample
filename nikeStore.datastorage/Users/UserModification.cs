using System;

namespace Nikestore.Datastorage.Users
{
	public sealed class UserModification
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public GenderType? Gender { get; set; }

		public DateTimeOffset? Birthday { get; set; }

		public string Password { get; set; }
	}
}
