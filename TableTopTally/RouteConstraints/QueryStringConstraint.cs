/* QueryStringConstraint.cs
 * Purpose: A route constraint for query string paramaters
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.12: Created
 */ 

using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace TableTopTally.RouteConstraints
{
    /// <summary>
    /// RouteConstaint for query string paramaters
    /// </summary>
    public class QueryStringConstraint : IRouteConstraint
    {
        private Regex Regex { get; set; }

        /// <summary>
        /// Constructor that takes a regex string to match against.
        /// If matched, the constraint returns true
        /// </summary>
        /// <param name="regex">Regex string to constrain the route by</param>
        public QueryStringConstraint(string regex)
        {
            Regex = new Regex(regex, RegexOptions.IgnoreCase);
        }

        public bool Match(
            HttpContextBase httpContext, Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            // Check if the parameterName is in the QueryString collection
            if (httpContext.Request.QueryString.AllKeys.Contains(parameterName))
            {
                // Validate the matching querystring against the specified regex
                return Regex.Match(httpContext.Request.QueryString[parameterName]).Success;
            }
            else if (values.ContainsKey(parameterName)) // Allows for ActionLink method to work properly
            {
                return Regex.Match(values[parameterName].ToString()).Success;
            }

            // Return false as the parameterName wasn't in the QueryString
            return false;
        }
    }
}