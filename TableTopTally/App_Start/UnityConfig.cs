/* UnityConfig.cs
 * Purpose: Setup file for Unity dependency injection
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.28: Created
 */ 

using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using TableTopTally.MongoDB.Services;

namespace TableTopTally.App_Start
{
    public class UnityConfig
    {
        public static UnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IGameService, GameService>(new HierarchicalLifetimeManager());

            return container;
        }
    }

    /// <summary>
    /// Implementation of IDependencyResolver that wraps a Unity container
    /// </summary>
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}