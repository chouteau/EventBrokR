using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBrokRCore.Tests
{
	class MyConsumer : EventBrokR.IConsumer<MyEvent>
	{
		public void Handle(MyEvent eventMessage)
		{
			Console.WriteLine(eventMessage);
			StaticContainer.Content = eventMessage.Name;
		}
	}
}
