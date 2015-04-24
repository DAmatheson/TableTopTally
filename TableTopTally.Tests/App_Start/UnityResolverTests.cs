using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using TableTopTally.MongoDataAccess.Services;
using Moq;
using NUnit.Framework;

namespace TableTopTally.Tests.App_Start
{
    [TestFixture]
    public class UnityResolverTests
    {
        private UnityContainer GetContainer()
        {
            return new UnityContainer();
        }

        private UnityContainer SetupUnityContainer<TFrom, TTo>() where TTo : TFrom
        {
            UnityContainer container = GetContainer();
            container.RegisterType<TFrom, TTo>();

            return container;
        }

        private UnityContainer SetupUnityContainer(Type from, Type to, string name)
        {
            UnityContainer container = GetContainer();
            container.RegisterType(from, to, name);

            return container;
        }

        private UnityContainer SetupUnityContainer(Type from, Type to)
        {
            UnityContainer container = GetContainer();
            container.RegisterType(from, to);

            return container;
        }

        private UnityResolver CreateUnityResolver(IUnityContainer container)
        {
            return new UnityResolver(container);
        }

        private ResolutionFailedException CreateResolutionFailedException<TBeingResolved>(string namedInstanceName = null)
        {
            // Ugly but seems to be simplest way to create/throw ResolutionFailedException
            return new ResolutionFailedException(typeof(TBeingResolved),
                null, null, new BuilderContext(null, null, null, null,
                    NamedTypeBuildKey.Make<TBeingResolved>(namedInstanceName), null));
        }

        [Test]
        public void Constructor_NullContainer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UnityResolver(null));
        }

        [Test]
        public void Dispose_WhenCalled_DisposesContainer()
        {
            Mock<IUnityContainer> mockContainer = new Mock<IUnityContainer>();

            UnityResolver resolver = CreateUnityResolver(mockContainer.Object);

            // Act
            resolver.Dispose();

            mockContainer.Verify(m => m.Dispose());
        }

        [Test]
        public void GetService_RegisteredType_ReturnsRegisteredDependency()
        {
            UnityContainer container = SetupUnityContainer<IGameService, GameService>();
            UnityResolver resolver = CreateUnityResolver(container);

            // Act
            object result = resolver.GetService(typeof (IGameService));

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<GameService>());
        }

        [Test]
        public void GetService_UnregisteredType_ReturnsNull()
        {
            UnityContainer container = GetContainer();
            UnityResolver resolver = CreateUnityResolver(container);

            // Act
            object result = resolver.GetService(typeof (IGameService));

            Assert.IsNull(result);
        }

        [Test]
        public void GetService_Null_ThrowsArgumentNullException()
        {
            UnityContainer container = SetupUnityContainer<IGameService, GameService>();
            UnityResolver resolver = CreateUnityResolver(container);

            Assert.Throws<ArgumentNullException>(() => resolver.GetService(null));
        }

        [Test]
        public void GetServices_NamedRegisteredType_ReturnsRegisteredDependency()
        {
            UnityContainer container = SetupUnityContainer(typeof (IGameService), typeof (GameService), "IGameService");
            UnityResolver resolver = CreateUnityResolver(container);

            // Act
            IEnumerable<object> results = resolver.GetServices(typeof (IGameService));

            Assert.IsNotNull(results);
            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results.FirstOrDefault(), Is.InstanceOf<GameService>());
        }

        [Test]
        public void GetServices_UnnamedRegisteredType_ReturnsEmptyIEnumerable()
        {
            UnityContainer container = SetupUnityContainer(typeof(IGameService), typeof(GameService));
            UnityResolver resolver = CreateUnityResolver(container);

            // Act
            IEnumerable<object> results = resolver.GetServices(typeof(IGameService));

            Assert.IsNotNull(results);
            Assert.That(results, Is.Empty);
        }

        [Test]
        public void GetServices_UnregisteredType_ReturnsEmptyIEnumerable()
        {
            UnityContainer container = SetupUnityContainer(typeof(IGameService), typeof (GameService), "IGameService" );
            UnityResolver resolver = CreateUnityResolver(container);

            // Act
            IEnumerable<object> results = resolver.GetServices(typeof (IFormattable));

            Assert.IsNotNull(results);
            Assert.That(results, Is.Empty);
        }

        [Test]
        public void GetServices_ResolutionFailedExceptionThrown_ReturnsEmptyIEnumerable()
        {
            Mock<IUnityContainer> stubContainer = new Mock<IUnityContainer>();
            stubContainer.Setup(s => s.ResolveAll(It.IsAny<Type>())).
                Throws(CreateResolutionFailedException<IFormattable>());

            UnityResolver resolver = CreateUnityResolver(stubContainer.Object);

            // Act
            IEnumerable<object> result = resolver.GetServices(typeof (IFormattable));

            Assert.IsNotNull(result);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetServices_Null_ThrowsArgumentNullException()
        {
            UnityContainer container = SetupUnityContainer(typeof (IGameService), typeof (GameService), "IGameService");
            UnityResolver resolver = CreateUnityResolver(container);

            Assert.Throws<ArgumentNullException>(() => resolver.GetServices(null));
        }

        [Test]
        public void BeginScope_ReturnsNewUnityResolver()
        {
            UnityContainer container = GetContainer();
            UnityResolver resolver = CreateUnityResolver(container);

            // Act
            IDependencyScope result = resolver.BeginScope();

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<UnityResolver>());
            Assert.That(result, Is.Not.EqualTo(resolver));
        }
    }
}