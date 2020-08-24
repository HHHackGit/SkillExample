using System;

namespace NikeStore.Critical
{
	public interface IApplicationCriticalServicesContext
	{
		T Get<T>(string serviceKey = default) where T : class;
	}
}
