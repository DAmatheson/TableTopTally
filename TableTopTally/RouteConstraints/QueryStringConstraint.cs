using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace TableTopTally.RouteConstraints
{
    public class QueryStringConstraint : IRouteConstraint
    {
        private Regex Regex { get; set; }

        public QueryStringConstraint(string regex)
        {
            Regex = new Regex(regex, RegexOptions.IgnoreCase);
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            // Check if the parameterName is in the QueryString collection
            if (httpContext.Request.QueryString.AllKeys.Contains(parameterName))
            {
                // Validate the matching querystring against the specified regex
                return Regex.Match(httpContext.Request.QueryString[parameterName]).Success;
            }

            // Return false as the parameterName wasn't in the QueryString
            return false;
        }
    }
}