using System;
using System.Collections.Generic;

using Autofac.Core;
using Autofac.Builder;

namespace NikeStore.Extensions.Autofac
{
	public interface IViewModelRegistration<out TView, out TActivator, out TRegistrationStyle>
	{
		IRegistrationBuilder<TView, TActivator, TRegistrationStyle> OfType<TViewModel>();

		IRegistrationBuilder<TView, TActivator, TRegistrationStyle> OfType<TViewModel>(params Parameter[] parameters);

		IRegistrationBuilder<TView, TActivator, TRegistrationStyle> OfType(Type viewModelType);

		IRegistrationBuilder<TView, TActivator, TRegistrationStyle> OfType(Type viewModelType, IEnumerable<Parameter> parameters);

		IRegistrationBuilder<TView, TActivator, TRegistrationStyle> OfType(Type viewModelType, params Parameter[] parameters);

		IRegistrationBuilder<TView, TActivator, TRegistrationStyle> Instance(object instance);
	}
}
