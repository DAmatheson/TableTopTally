﻿/* UnityConfig.cs
 * Purpose: Setup file for Unity dependency injection
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.28
 */

using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using TableTopTally.MongoDataAccess.Services;

namespace TableTopTally
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
    // From: http://www.asp.net/web-api/overview/advanced/dependency-injection
    public class UnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;

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