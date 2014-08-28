using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;
using MongoDB.Bson;
using TableTopTally.Binders;
using TableTopTally.Helpers;
using TableTopTally.RouteConstraints;

namespace TableTopTally
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Replace ActionValueBinder with CustomActionValueBinder which uses [FromUri] on GET
            config.Services.Replace(typeof(IActionValueBinder), new CustomActionValueBinder());

            var objectIdProvider = new SimpleModelBinderProvider(typeof(ObjectId), new ObjectIdApiBinder());

            // Add the ObjectId binder to the list of binders
            config.Services.Insert(typeof(ModelBinderProvider), 0, objectIdProvider);

            var constrainResolver = new DefaultInlineConstraintResolver();

            // Add the ObjectId constraint with the name as objectId
            constrainResolver.ConstraintMap.Add("objectId", typeof(ObjectIdApiConstaint));

            // Web API routes
            config.MapHttpAttributeRoutes(constrainResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
