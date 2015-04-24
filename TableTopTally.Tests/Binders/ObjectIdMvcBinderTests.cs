using System.Collections.Specialized;
using System.Web.Mvc;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.Binders;
using TableTopTally.Tests.TestingHelpers;

namespace TableTopTally.Tests.Binders
{
    [TestFixture]
    public class ObjectIdMvcBinderTests
    {
        [Test]
        public void BindModel_WithValidObjectId_ReturnsMatchingObjectId()
        {
            var formCollection = new NameValueCollection
            {
                { "Id", "53e3a8ad6c46bc0c80ea13b2" }
            };

            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(FakeMongoEntity));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Id",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            ObjectIdMvcBinder binder = new ObjectIdMvcBinder();

            ControllerContext controllerContext = new ControllerContext();

            // Act
            ObjectId? result = binder.BindModel(controllerContext, bindingContext) as ObjectId?;

            Assert.IsNotNull(result);
            Assert.AreEqual(new ObjectId("53e3a8ad6c46bc0c80ea13b2"), result.Value);
        }

        [Test]
        public void BindModel_WithInvalidObjectId_ReturnsEmptyObjectId()
        {
            var formCollection = new NameValueCollection
            {
                { "Id", " " }
            };

            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(FakeMongoEntity));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Id",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            ObjectIdMvcBinder binder = new ObjectIdMvcBinder();

            ControllerContext controllerContext = new ControllerContext();

            // Act
            ObjectId? result = binder.BindModel(controllerContext, bindingContext) as ObjectId?;

            Assert.IsNotNull(result);
            Assert.AreEqual(ObjectId.Empty, result.Value);
        }

        [Test]
        public void BindModel_WithNull_ReturnsEmptyObjectId()
        {
            var formCollection = new NameValueCollection
            {
                { "Id", null }
            };

            var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(FakeMongoEntity));

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Id",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            ObjectIdMvcBinder binder = new ObjectIdMvcBinder();

            ControllerContext controllerContext = new ControllerContext();

            // Act
            ObjectId? result = binder.BindModel(controllerContext, bindingContext) as ObjectId?;

            Assert.IsNotNull(result);
            Assert.AreEqual(ObjectId.Empty, result.Value);
        }
    }
}
