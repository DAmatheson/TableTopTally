using Microsoft.Practices.Unity;
using TableTopTally.MongoDataAccess.Services;
using NUnit.Framework;

namespace TableTopTally.Tests.App_Start
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
