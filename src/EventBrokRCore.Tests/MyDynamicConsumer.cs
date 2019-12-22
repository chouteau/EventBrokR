using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBrokRCore.Tests
{
	class MyDynamicConsumer : EventBrokR.IConsumer<System.Dynamic.ExpandoObject>
	{
		public void Handle(System.Dynamic.ExpandoObject eventMessage)
		{
			dynamic d = eventMessage;
			StaticContainer.Content = d.Message;
		}
	}
}
