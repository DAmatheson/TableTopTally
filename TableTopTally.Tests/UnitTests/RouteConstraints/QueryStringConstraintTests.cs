using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using TableTopTally.RouteConstraints;

namespace TableTopTally.Tests.UnitTests.RouteConstraints
{
    [TestFixture]
    class QueryStringConstraintTests
    {
        [Test]
        public void Match_WithMatchingValue_ReturnsTrue()
        {
            string pattern = "True";
            string parameterName = "testing";

            var httpContextStub = new Mock<HttpContextBase>();
            httpContextStub.Setup(s => s.Request.QueryString).Returns(new NameValueCollection
            {
                { parameterName, "true" }
            });

            QueryStringConstraint constraint = new QueryStringConstraint(pattern);

            // Act
            bool result = constraint.Match(httpContextStub.Object, null, parameterName, null, RouteDirection.IncomingRequest);

            Assert.IsTrue(result);
        }

        [Test]
        public void Match_WithoutMatchingValue_ReturnsFalse()
        {
            string pattern = "True";
            string parameterName = "testing";

            var httpContextStub = new Mock<HttpContextBase>();
            httpContextStub.Setup(s => s.Request.QueryString).Returns(new NameValueCollection
            {
                { parameterName, "false" }
            });

            QueryStringConstraint constraint = new QueryStringConstraint(pattern);

            // Act
            bool result = constraint.Match(httpContextStub.Object, null, parameterName, null, RouteDirection.IncomingRequest);

            Assert.IsFalse(result);
        }

        [Test]
        public void Match_RequestWithoutMatchingParameter_ReturnsFalse()
        {
            string pattern = "True";
            string parameterName = "testing";

            var httpContextStub = new Mock<HttpContextBase>();
            httpContextStub.Setup(s => s.Request.QueryString).Returns(new NameValueCollection());

            var routeValues = new RouteValueDictionary();

            QueryStringConstraint constraint = new QueryStringConstraint(pattern);

            // Act
            bool result = constraint.Match(httpContextStub.Object, null, parameterName, routeValues, RouteDirection.IncomingRequest);

            Assert.IsFalse(result);
        }

        [Test]
        public void Match_RequestWithMatchingRouteValue_ReturnsTrue()
        {
            string pattern = "True";
            string parameterName = "testing";

            var httpContextStub = new Mock<HttpContextBase>();
            httpContextStub.Setup(s => s.Request.QueryString).Returns(new NameValueCollection());

            var routeValues = new RouteValueDictionary
            {
                { parameterName, "true" }
            };

            QueryStringConstraint constraint = new QueryStringConstraint(pattern);

            // Act
            bool result = constraint.Match(httpContextStub.Object, null, parameterName, routeValues, RouteDirection.IncomingRequest);

            Assert.IsTrue(result);
        }

        [Test]
        public void Match_RequestWithoutMatchingRouteValue_ReturnsFalse()
        {
            string pattern = "True";
            string parameterName = "testing";

            var httpContextStub = new Mock<HttpContextBase>();
            httpContextStub.Setup(s => s.Request.QueryString).Returns(new NameValueCollection());

            var routeValues = new RouteValueDictionary
            {
                { parameterName, "false" }
            };

            QueryStringConstraint constraint = new QueryStringConstraint(pattern);

            // Act
            bool result = constraint.Match(httpContextStub.Object, null, parameterName, routeValues, RouteDirection.IncomingRequest);

            Assert.IsFalse(result);
        }
    }
}
