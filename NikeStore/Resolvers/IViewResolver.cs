using System;
using System.Windows;

namespace NikeStore.Resolvers
{
	public interface IViewResolver
	{
		T Resolve<T>(params object[] parameters)
			where T : FrameworkElement;
	}
}
