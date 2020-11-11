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
		private readonly IServiceCollection m_ServiceCollection;
		internal Container(IServiceCollection serviceCollection)
		{
			m_ServiceCollection = serviceCollection;
			this.Subscriptions = new List<object>();
		}

		public IEnumerable<Type> Registrations => m_ServiceCollection.Select(i => i.ServiceType);

		public void Register<TConsumer>()
		{
			var t = typeof(TConsumer);
			m_ServiceCollection.AddTransient(t);
		}

		internal IList<object> Subscriptions { get; set; }
	}
}
