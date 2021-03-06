﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventBrokR
{
	public interface IDependencyResolver
	{
		object GetService(Type serviceType);
		IEnumerable<object> GetServices(Type serviceType);
		IEnumerable<object> GetAllServices();
	}
}
