using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;

namespace EventBrokR
{
	/// <summary>
	/// Inspired by :
	/// http://elegantcode.com/2010/01/06/event-driven-architecture-publishing-events-using-an-ioc-container/
	/// </summary>
	public class Publisher : IPublisher
	{
		private Lazy<Container> m_Container;

		public Publisher()
		{
			m_Container = new Lazy<Container>(CreateContainer);
		}

		public Container Container
		{
			get
			{
				return m_Container.Value;
			}
		}

		public virtual Task PublishAsync<T>(T eventMessage, int delay = 0)
		{
			// For tests
			if (GlobalConfiguration.Configuration.SynchronizedPublication)
			{
				Publish(eventMessage);
				return Task.Delay(0);
			}

			Task result = null;
			if (delay > 0)
			{
				result = Task.Run(async delegate
				{
					await Task.Delay(delay);
					Publish(eventMessage);
				});
			}
			else
			{
				result = Task.Run(() =>
				{
					Publish(eventMessage);
				});
			}
			return result;
		}

		public virtual dynamic CreateDynamicMessage(string messageName)
		{
			if (string.IsNullOrWhiteSpace(messageName))
			{
				throw new ArgumentException("messageName does not null or empty");
			}
			dynamic result = new System.Dynamic.ExpandoObject();
			result.Name = messageName;
			return result;
		}

		protected virtual void Publish<T>(T eventMessage)
		{
			// GlobalConfiguration.Configuration.Logger.Debug("Publish eventMessage : {0}", eventMessage.GetType().FullName);
			var subscriptions = GetSubscriptions<T>();
			if (subscriptions == null || subscriptions.Count() == 0)
			{
				// GlobalConfiguration.Configuration.Logger.Debug("No subscriber found for event message");
				return;
			}

			foreach (var subscription in subscriptions)
			{
				// GlobalConfiguration.Configuration.Logger.Debug("Publish event message to subscriber : {0}", subscription.GetType().FullName);
				PublishToConsumer(subscription, eventMessage);
			}
		}

		private void PublishToConsumer<T>(Consumer<T> x, T eventMessage)
		{
			try
			{
				x.Instance.Handle(eventMessage);
				// GlobalConfiguration.Configuration.Logger.Debug("eventMessage sent to : {0}", x.GetType().FullName);
			}
			catch (Exception ex)
			{
				GlobalConfiguration.Configuration.Logger.Error(ex);
			}
			finally
			{
				var instance = x as IDisposable;
				if (instance != null)
				{
					instance.Dispose();
				}
			}
		}

		internal IEnumerable<Consumer<T>> GetSubscriptions<T>()
		{
			var result = new List<Consumer<T>>();

			var consumerInterfaceName = typeof(IConsumer<T>).FullName;
			var consumers = from type in m_Container.Value.Registrations
							from itf in type.GetInterfaces()
							where itf.FullName.Equals(consumerInterfaceName, StringComparison.InvariantCultureIgnoreCase)
							select m_Container.Value.Resolve<T>(type);

			result.AddRange(consumers);

			var anonymousSubscriptions = from subscription in m_Container.Value.Subscriptions
										 select new Consumer<T>((IConsumer<T>)subscription);

			result.AddRange(anonymousSubscriptions);

			return result;
		}

		protected virtual Container CreateContainer()
		{
			var container = new Container();
			return container;
		}

	}
}
