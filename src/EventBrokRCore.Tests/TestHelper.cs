using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using EventBrokR;

namespace EventBrokRCore.Tests
{
	public class TestHelper
	{
		private readonly IServiceProvider m_ServiceProvider;
		private readonly IServiceCollection m_ServiceCollection;

		private static Lazy<TestHelper> m_LazyTests = new Lazy<TestHelper>(() =>
		{
			var result = new TestHelper();
			return result;
		});

		private TestHelper()
		{
			m_ServiceCollection = new ServiceCollection();

			m_ServiceCollection.AddEventBrokRServices();

			var loggerFactory = new LoggerFactory();
			m_ServiceCollection.AddSingleton(loggerFactory);
			m_ServiceCollection.AddLogging();

			m_ServiceProvider = m_ServiceCollection.BuildServiceProvider();
		}

		public static TestHelper Current
		{
			get
			{
				return m_LazyTests.Value;
			}
		}

		public IServiceCollection Services
		{
			get
			{
				return m_ServiceCollection;
			}
		}

		public T GetService<T>()
		{
			T result = default(T);
			var scope = m_ServiceProvider.CreateScope();
			try
			{
				result = scope.ServiceProvider.GetRequiredService<T>();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			return result;
		}


	}
}
