using System;

namespace Support
{
	sealed class Verify
	{
		public static class Arguments
		{
			public static void IsNotNull<T>(T value, string parameterName)
			{
				if(value == null)
				{
					throw new ArgumentNullException(nameof(parameterName));
				}
			}

			public static void IsNull<T>(T value, string parameterName)
			{
				if(value != null)
				{
					throw new ArgumentException(nameof(parameterName));
				}
			}
		}
	}
}
