using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TableTopTally.Helpers;
using MongoDB.Bson;

namespace TableTopTally
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Add a binder for ObjectIds
            ModelBinders.Binders.Add(typeof(ObjectId), new ObjectIdBinder());

            // Formatter for camelCase outgoing json properties
            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            //settings.Formatting = Formatting.Indented; // Makes json properly indented so it is readable
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
