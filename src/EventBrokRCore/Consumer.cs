using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBrokR
{
	class Consumer<T> 
	{
		public Consumer(IConsumer<T> instance)
		{
			Instance = instance;
		}

		public IConsumer<T> Instance { get; private set; }
	}
}
