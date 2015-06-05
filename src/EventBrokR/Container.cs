using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBrokR
{
	public class Container
	{
		internal Container()
		{
			this.Registrations = new List<Type>();
			this.Subscriptions = new List<object>();
		}

		internal IList<Type> Registrations { get; set; }
		internal IList<object> Subscriptions { get; set; }

		internal Consumer<T> Resolve<T>(Type t)
		{
			var instance = (IConsumer<T>) GlobalConfiguration.Configuration.DependencyResolver.GetService(t);
			return new Consumer<T>(instance);
		}
	}
}
