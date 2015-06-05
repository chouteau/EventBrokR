using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBrokR.Tests
{
	public class MyDynConsumer : EventBrokR.DynamicConsumerBase
	{
		protected override void HandleMessage(string messageName, dynamic message)
		{
			if (messageName == "Test")
			{
				StaticContainer.Content = message.Content;
			}
		}
	}
}
