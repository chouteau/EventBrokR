using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBrokR
{
	/// <summary>
	/// source :
	/// http://elegantcode.com/2010/01/06/event-driven-architecture-publishing-events-using-an-ioc-container/
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IConsumer<TEvent>
	{
		Task HandleAsync(TEvent eventMessage);
	}
}
