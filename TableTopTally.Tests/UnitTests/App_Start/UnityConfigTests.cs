using Microsoft.Practices.Unity;
using NUnit.Framework;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.Tests.UnitTests.App_Start
{
    [TestFixture]
    public class UnityConfigTests
    {
        [Test]
        public void GetContainer_WhenCalled_ReturnsUnityContainerWithIGameServiceRegistered()
        {
            UnityContainer container = UnityConfig.GetContainer();

            // Act
            Assert.IsTrue(container.IsRegistered<IGameService>());
        }
    }
}
