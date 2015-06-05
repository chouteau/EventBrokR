using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBrokR.Tests
{
	class MyConsumer : IConsumer<MyEvent>
	{
		public void Handle(MyEvent eventMessage)
		{
			Console.WriteLine(eventMessage);
			StaticContainer.Content = eventMessage.Name;
		}
	}
}
