using System;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

using Support;

using Autofac;
using Autofac.Core;

namespace NikeStore.Resolvers
{
	sealed class ViewResolver : IViewResolver
	{
		#region Data

		private readonly IComponentContext _componentContext;

		#endregion

		#region .ctor

		public ViewResolver(IComponentContext componentContext)
		{
			Verify.Arguments.IsNotNull(componentContext, nameof(componentContext));

			_componentContext = componentContext;
		}

		#endregion

		#region Private methods

		private static IEnumerable<Parameter> ConvertParameters(object[] parameters)
		{
			return parameters.Select(p => p is Parameter parameter
				? parameter
				: new TypedParameter(p.GetType(), p));
		}

		#endregion

		#region IViewResolver implementation

		public T Resolve<T>(params object[] parameters) where T : FrameworkElement
		{
			return _componentContext.Resolve<T>(ConvertParameters(parameters));
		}

		#endregion
	}
}
