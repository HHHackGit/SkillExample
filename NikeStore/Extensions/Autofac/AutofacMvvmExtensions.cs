using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NikeStore.Extensions.Autofac
{
	public static partial class AutofacMvvmExtensions
	{
		public static IViewModelRegistration<TView, ConcreteReflectionActivatorData, SingleRegistrationStyle> WithViewModel<TView>(this IRegistrationBuilder<TView, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder)
			where TView : FrameworkElement
		{
			Verify.Arguments.IsNotNull(registrationBuilder, nameof(registrationBuilder));

			return new ViewModelRegistration<TView>(registrationBuilder);
		}

		/// <summary>Присоединяет модель представления к generic-представлению.</summary>
		/// <param name="registrationBuilder"></param>
		/// <returns></returns>
		public static IViewModelRegistration<object, ReflectionActivatorData, DynamicRegistrationStyle> WithViewModel(this IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> registrationBuilder)
		{
			Verify.Arguments.IsNotNull(registrationBuilder, nameof(registrationBuilder));

			return new ViewModelRegistration(registrationBuilder);
		}

		/// <summary>Возвращает экземпляр объекта тип <typeparamref name="T" /> из параметров или контейнера.</summary>
		/// <typeparam name="T">Тип возвращаемого объекта.</typeparam>
		/// <param name="parameters">Список параметров.</param>
		/// <param name="componentContext">Контекст операции разрешения зависимости.</param>
		/// <returns>Объект типа <typeparamref name="T" />.</returns>
		private static T FromParamsOrResolve<T>(IEnumerable<Parameter> parameters, IComponentContext componentContext)
		{
			if(parameters != null)
			{
				foreach(var p in parameters)
				{
					if(p is TypedParameter typed &&
					   (typed.Type.IsGenericType && typed.Type.GetGenericTypeDefinition() == typeof(T)
						|| typed.Type == typeof(T)))
					{
						return (T)typed.Value;
					}
				}
			}
			return componentContext.Resolve<T>(parameters);
		}

		private static object FromParamsOrResolve(Type type, IEnumerable<Parameter> parameters, IComponentContext componentContext)
		{
			if(parameters != null)
			{
				foreach(var p in parameters)
				{
					if(p is TypedParameter typed &&
					   (typed.Type.IsGenericType && typed.Type.GetGenericTypeDefinition() == type
						|| typed.Type == type))
					{
						return typed.Value;
					}
				}
			}
			return componentContext.Resolve(type, parameters);
		}
	}
}
