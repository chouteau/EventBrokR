using Microsoft.VisualStudio.TestTools.UnitTesting;

using EventBrokR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace EventBrokRCore.Tests
{
	[TestClass]
	public class SubscriptionTests
	{
		[ClassInitialize]
		public void Setup()
		{
			Publisher = TestHelper.Current.GetService<EventBrokR.IPublisher>();
		}

		protected EventBrokR.IPublisher Publisher { get; set; }

		[TestMethod]
		public async Task Handle_Event()
		{
			StaticContainer.Content = null;
			await Publisher.PublishAsync(new MyEvent() { Name = "Test" });

			Assert.AreEqual(StaticContainer.Content, "Test");
		}

		[TestMethod]
		public async Task Dynamic_Handle_Event()
		{
			StaticContainer.Content = null;
			dynamic d = new System.Dynamic.ExpandoObject();
			d.Message = "Test";
			await Publisher.PublishAsync(d);

			Assert.AreEqual(StaticContainer.Content, "Test");
		}

		[TestMethod]
		public async Task Handle_Event_With_Anonymous_Consumer()
		{
			StaticContainer.Content = null;
			Publisher.Subscribe<MyEvent>(@event =>
			{
				StaticContainer.Content = @event.Name;
			});

			await Publisher.PublishAsync(new MyEvent() { Name = "Test" });

			Assert.AreEqual(StaticContainer.Content, "Test");
		}


	}
}
