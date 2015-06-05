using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventBrokR
{
	public class EventBrokRConfiguration
	{
		public EventBrokRConfiguration()
		{
			SynchronizedPublication = false;
		}

		public ILogger Logger { get; set; }
		public IDependencyResolver DependencyResolver { get; set; }
		/// <summary>
		/// Set this property to true in test context
		/// </summary>
		public bool SynchronizedPublication { get; set; }
	}
}
