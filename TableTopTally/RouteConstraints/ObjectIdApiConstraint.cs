/* ObjectIdApiConstraint.cs
 * Purpose: Web API 2 route parameter constraint for ObjectIds
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.28: Created
 */ 

using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Http.Routing;

namespace TableTopTally.RouteConstraints
{
    /// <summary>
    /// Restricts a route to valid ObjectId values
    /// </summary>
    public class ObjectIdApiConstaint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;

            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                ObjectId id;

                if (value is ObjectId)
                {
                    id = (ObjectId) value;

                    return id != ObjectId.Empty;
                }

                string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);

                if (ObjectId.TryParse(valueString, out id))
                {
                    return id != ObjectId.Empty;
                }
            }

            return false;
        }
    }
}