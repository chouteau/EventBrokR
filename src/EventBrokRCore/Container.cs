using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace EventBrokR
{
	public class Container
	{
		internal Container(IServiceCollection serviceCollection)
		{
			this.ServiceCollection = serviceCollection;
			this.Subscriptions = new List<object>();
		}

		internal IServiceCollection ServiceCollection { get; set; }

		public IEnumerable<Type> Registrations => ServiceCollection.Select(i => i.ServiceType);

		public void Register<TConsumer>()
		{
			var t = typeof(TConsumer);
			ServiceCollection.AddTransient(t);
		}

		public void Register(Type t)
		{
			ServiceCollection.AddTransient(t);
		}

		internal IList<object> Subscriptions { get; set; }
	}
}
