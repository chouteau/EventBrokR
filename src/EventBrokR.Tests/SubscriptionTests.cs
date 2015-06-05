using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventBrokR.Tests
{
	[TestClass]
	public class SubscriptionTests
	{
		[Microsoft.VisualStudio.TestTools.UnitTesting.TestInitialize]
		public void Setup()
		{
			Publisher = new Publisher();
			Publisher.Container.Register<MyConsumer>();
			Publisher.Container.Register<MyDynamicConsumer>();
		}

		protected IPublisher Publisher { get; set; }

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
		public void Handle_Event_With_Delay()
		{
			StaticContainer.Content = null;
			var start = DateTime.Now;
			var duration = 0;
			Publisher.PublishAsync(new MyEvent() { Name = "Test" }, 2 * 1000).ContinueWith(task =>
				{
					duration = Convert.ToInt32((DateTime.Now - start).TotalSeconds);
				}).Wait();

			Assert.AreEqual(StaticContainer.Content, "Test");
			Assert.AreEqual(duration, 2);
		}

		[TestMethod]
		public void Handle_Event_With_Anonymous_Consumer()
		{
			StaticContainer.Content = null;
			Publisher.Container.Subscribe<MyEvent>(@event =>
				{
					StaticContainer.Content = @event.Name;
				});

			Publisher.PublishAsync(new MyEvent() { Name = "Test" }).Wait();

			Assert.AreEqual(StaticContainer.Content, "Test");
		}

		[TestMethod]
		public void Handle_Event_With_Dymanic_Consumer()
		{
			StaticContainer.Content = null;
			Publisher.Container.Register<MyDynConsumer>();

			dynamic message = Publisher.CreateDynamicMessage("Test");
			message.Content = "Dynamic content";

			Publisher.PublishAsync(message).Wait();

			Assert.AreEqual(StaticContainer.Content, "Dynamic content");
		}

		[TestMethod]
		public void Synchronized_Event_Enabled()
		{
			GlobalConfiguration.Configuration.SynchronizedPublication = true;
			StaticContainer.Content = null;
			dynamic d = Publisher.CreateDynamicMessage("Test");
			d.Message = "Test";
			Publisher.PublishAsync(d);

			Assert.AreEqual(StaticContainer.Content, "Test");
		}

		[TestMethod]
		public void Registration_List()
		{
			var list = Publisher.Container.GetRegistrationList();

			Assert.IsNotNull(list);
		}
	}
}
