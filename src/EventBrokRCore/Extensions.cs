using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace EventBrokR
{
	public static class Extensions
	{
		public static IServiceCollection AddEventBrokRServices(this IServiceCollection services, Action<Container> containerExpression)
		{
			var container = new Container(services);
			containerExpression.Invoke(container);
			services.AddSingleton(container);
			services.AddSingleton<IPublisher, Publisher>();
			return services;
		}

		public static IServiceCollection AddEventBrokRServices(this IServiceCollection services, Container container)
		{
			container.ServiceCollection = services;
			services.AddSingleton(container);
			services.AddSingleton<IPublisher, Publisher>();
			return services;
		}


		public static bool IsDynamicPropertyExists(this System.Dynamic.ExpandoObject obj, string propertyName)
		{
			if (obj == null)
			{
				return false;
			}
			return ((IDictionary<String, object>)obj).ContainsKey(propertyName);
		}

		public static IEnumerable<string> GetRegistrationList(this Container container)
		{
			return container.Registrations.Select(i => i.AssemblyQualifiedName);
		}
	}
}
