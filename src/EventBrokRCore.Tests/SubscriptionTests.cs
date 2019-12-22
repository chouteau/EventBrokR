using Microsoft.VisualStudio.TestTools.UnitTesting;

using EventBrokR;
using Microsoft.Extensions.DependencyInjection;

namespace EventBrokRCore.Tests
{
	[TestClass]
	public class SubscriptionTests
	{
		[TestInitialize]
		public void Setup()
		{
			Publisher = TestHelper.Current.GetService<EventBrokR.IPublisher>();
			Publisher.RegisterConsumer<MyConsumer>();
			Publisher.RegisterConsumer<MyDynamicConsumer>();
		}

		protected EventBrokR.IPublisher Publisher { get; set; }

		[TestMethod]
		public void Handle_Event()
		{
			StaticContainer.Content = null;
			Publisher.PublishAsync(new MyEvent() { Name = "Test" }).Wait();

			Assert.AreEqual(StaticContainer.Content, "Test");
		}

		[TestMethod]
		public void Dynamic_Handle_Event()
		{
			StaticContainer.Content = null;
			dynamic d = new System.Dynamic.ExpandoObject();
			d.Message = "Test";
			Publisher.PublishAsync(d).Wait();

			Assert.AreEqual(StaticContainer.Content, "Test");
		}

		[TestMethod]
		public void Handle_Event_With_Anonymous_Consumer()
		{
			StaticContainer.Content = null;
			Publisher.Subscribe<MyEvent>(@event =>
			{
				StaticContainer.Content = @event.Name;
			});

			Publisher.PublishAsync(new MyEvent() { Name = "Test" }).Wait();

			Assert.AreEqual(StaticContainer.Content, "Test");
		}


	}
}
