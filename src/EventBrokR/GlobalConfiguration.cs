using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventBrokR
{
	public class GlobalConfiguration
	{
		private static object m_Lock = new object();
		private static EventBrokRConfiguration m_Configuration;

		public static EventBrokRConfiguration Configuration
		{
			get
			{
				if (m_Configuration == null)
				{
					lock (m_Lock)
					{
						if (m_Configuration == null)
						{
							m_Configuration = new EventBrokRConfiguration();
							m_Configuration.Logger = new DiagnosticsLogger();
							m_Configuration.DependencyResolver = new DefaultDependencyResolver();
						}
					}
				}
				return m_Configuration;
			}
		}
	}
}
