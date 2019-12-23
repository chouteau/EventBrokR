using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBrokR
{
	/// <summary>
	/// http://elegantcode.com/2010/01/06/event-driven-architecture-publishing-events-using-an-ioc-container/
	/// </summary>
	public interface IPublisher
	{
		void Subscribe<TMessage>(Action<TMessage> predicate);
		Task PublishAsync<TEvent>(TEvent eventMessage, int delay = 0);
		dynamic CreateDynamicMessage(string name);
	}
}
