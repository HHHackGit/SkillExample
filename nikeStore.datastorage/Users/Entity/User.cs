using Datastorage.Framework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Nikestore.Datastorage.Users
{
	public sealed class User : IIdentifiable<long>
	{
		#region Helpers

		private sealed class Configuration : EntityTypeConfiguration<User>
		{
			#region .ctor

			public Configuration()
			{
				ToTable("Users");

				HasKey(e => e.Id);
				Property(e => e.Id)
					.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

				Property(e => e.Name)
					.IsFixedLength()
					.HasMaxLength(Knowledge.User.NameLength)
					.IsRequired();

				Property(e => e.Surname)
					.IsFixedLength()
					.HasMaxLength(Knowledge.User.SurnameLength)
					.IsRequired();

				Property(e => e.Gender)
					.IsRequired();

				Property(e => e.Birthday)
					.IsRequired();

				Property(e => e.Mail)
					.IsRequired();

				Property(e => e.Password)
					.IsRequired();
			}

			#endregion
		}

		#endregion

		#region Static

		public static EntityTypeConfiguration<User> GetConfiguration() => new Configuration();

		#endregion

		#region Properties

		public long Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public GenderType Gender { get; set; }

		public DateTimeOffset Birthday { get; set; }

		public string Mail { get; set; }

		public string Password { get; set; }

		#endregion
	}
}
