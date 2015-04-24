using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.RouteConstraints;

namespace TableTopTally.Tests.RouteConstraints
{
    [TestFixture]
    class ObjectIdApiConstraintTests
    {
        private const string STRING_OBJECT_ID = "53e3a8ad6c46bc0c80ea13b2";

        [Test]
        public void Match_ValidObjectId_ReturnsTrue()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            HttpRoute route = new HttpRoute();
            string parameterName = "Id";
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "Id", new ObjectId(STRING_OBJECT_ID) }
            };

            ObjectIdApiConstaint constraint = new ObjectIdApiConstaint();

            // Act
            bool result = constraint.Match(request, route, parameterName, values, HttpRouteDirection.UriResolution);

            Assert.IsTrue(result);
        }

        [Test]
        public void Match_ValidObjectIdString_ReturnsTrue()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            HttpRoute route = new HttpRoute();
            string parameterName = "Id";
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "Id", STRING_OBJECT_ID }
            };

            ObjectIdApiConstaint constraint = new ObjectIdApiConstaint();

            // Act
            bool result = constraint.Match(request, route, parameterName, values, HttpRouteDirection.UriResolution);

            Assert.IsTrue(result);
        }

        [Test]
        public void Match_EmptyObjectId_ReturnsFalse()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            HttpRoute route = new HttpRoute();
            string parameterName = "Id";
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "Id", ObjectId.Empty }
            };

            ObjectIdApiConstaint constraint = new ObjectIdApiConstaint();

            // Act
            bool result = constraint.Match(request, route, parameterName, values, HttpRouteDirection.UriResolution);

            Assert.IsFalse(result);
        }

        [Test]
        public void Match_InvalidObjectIdString_ReturnsFalse()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            HttpRoute route = new HttpRoute();
            string parameterName = "Id";
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "Id", "notAnObjectId" }
            };

            ObjectIdApiConstaint constraint = new ObjectIdApiConstaint();

            // Act
            bool result = constraint.Match(request, route, parameterName, values, HttpRouteDirection.UriResolution);

            Assert.IsFalse(result);
        }

        [Test]
        public void Match_NullParameterValue_ReturnsFalse()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            HttpRoute route = new HttpRoute();
            string parameterName = "Id";
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "Id", null }
            };

            ObjectIdApiConstaint constraint = new ObjectIdApiConstaint();

            // Act
            bool result = constraint.Match(request, route, parameterName, values, HttpRouteDirection.UriResolution);

            Assert.IsFalse(result);
        }

        [Test]
        public void Match_UnmatchedParameterName_ReturnsFalse()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            HttpRoute route = new HttpRoute();
            string parameterName = "Id";
            Dictionary<string, object> values = new Dictionary<string, object>();

            ObjectIdApiConstaint constraint = new ObjectIdApiConstaint();

            // Act
            bool result = constraint.Match(request, route, parameterName, values, HttpRouteDirection.UriResolution);

            Assert.IsFalse(result);
        }
    }
}
