using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBrokR
{
	public static class Extensions
	{
		public static void Register<TConsumer>(this Container container)
		{
			container.Registrations.Add(typeof(TConsumer));
		}

		public static void Subscribe<TMessage>(this Container container, Action<TMessage> predicate)
		{
			var consumer = new AnonymousConsumer<TMessage>(predicate);
			container.Subscriptions.Add(consumer);
		}

		public static TService GetService<TService>(this IDependencyResolver resolver)
		{
			return (TService)resolver.GetService(typeof(TService));
		}

		public static IEnumerable<TService> GetServices<TService>(this IDependencyResolver resolver)
		{
			return resolver.GetServices(typeof(TService)).Cast<TService>();
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
