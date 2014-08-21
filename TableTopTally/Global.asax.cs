using MongoDB.Bson;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TableTopTally.Helpers;

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

            // Setting up the json formatter
            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            
            settings.Converters.Add(new ObjectIdConverter()); // Add json -> ObjectId converter
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //settings.Formatting = Formatting.Indented; // Makes json properly indented so it is readable
        }
    }
}
