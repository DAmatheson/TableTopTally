using MongoDB.Bson;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;
using TableTopTally.Binders;
using TableTopTally.RouteConstraints;

namespace TableTopTally
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Set up Unity for dependency injection
            config.DependencyResolver = new UnityResolver(UnityConfig.GetContainer());

            // Replace ActionValueBinder with FromUriOnGetActionValueBinder which uses [FromUri] on GET
            config.Services.Replace(typeof(IActionValueBinder), new FromUriOnGetActionValueBinder());

            // Add the ObjectId binder to the list of binders
            var objectIdProvider = new SimpleModelBinderProvider(typeof(ObjectId), new ObjectIdApiBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, objectIdProvider);

            // Add the ObjectId constraint with the name as objectId
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("objectId", typeof(ObjectIdApiConstaint));

            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
