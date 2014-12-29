using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Metadata.Providers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders.Providers;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.Binders;

namespace TableTopTally.Tests.UnitTests.Binders
{
    [TestFixture]
    public class ObjectIdApiBinderTests
    {
        [Test]
        public void BindModel_WithValidObjectId_ReturnsTrue()
        {
            var formCollection = new Dictionary<string, string>
            {
                { "Id", "53e3a8ad6c46bc0c80ea13b2" }
            };

            var valueProvider = new NameValuePairsValueProvider(formCollection, null);
            var modelMetadata = new ModelMetadata(new DataAnnotationsModelMetadataProvider(), null, null, typeof(ObjectId), "Id");

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Id",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            ObjectIdApiBinder binder = new ObjectIdApiBinder();

            HttpActionContext controllerContext = new HttpActionContext();

            // Act
            bool result = binder.BindModel(controllerContext, bindingContext);

            Assert.IsTrue(result);
        }

        [Test]
        public void BindModel_WithValidObjectId_SetsModelValue()
        {
            var formCollection = new Dictionary<string, string>
            {
                { "Id", "53e3a8ad6c46bc0c80ea13b2" }
            };

            var valueProvider = new NameValuePairsValueProvider(formCollection, null);
            var modelMetadata = new ModelMetadata(new DataAnnotationsModelMetadataProvider(), null, null, typeof(ObjectId), "Id");

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Id",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            ObjectIdApiBinder binder = new ObjectIdApiBinder();

            HttpActionContext controllerContext = new HttpActionContext();

            // Act
            binder.BindModel(controllerContext, bindingContext);

            Assert.That(bindingContext.Model, Is.EqualTo(new ObjectId("53e3a8ad6c46bc0c80ea13b2")));
        }

        [Test]
        public void BindModel_WithInvalidObjectId_ReturnsFalse()
        {
            var formCollection = new Dictionary<string, string>
            {
                { "Id", " " }
            };

            var valueProvider = new NameValuePairsValueProvider(formCollection, null);
            var modelMetadata = new ModelMetadata(new DataAnnotationsModelMetadataProvider(), null, null, typeof(ObjectId), "Id");

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Id",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            ObjectIdApiBinder binder = new ObjectIdApiBinder();

            HttpActionContext controllerContext = new HttpActionContext();

            // Act
            bool result = binder.BindModel(controllerContext, bindingContext);

            Assert.IsFalse(result);
            Assert.IsNull(bindingContext.Model);
        }

        [Test]
        public void BindModel_WithInvalidObjectId_LeavesNullModel()
        {
            var formCollection = new Dictionary<string, string>
            {
                { "Id", " " }
            };

            var valueProvider = new NameValuePairsValueProvider(formCollection, null);
            var modelMetadata = new ModelMetadata(new DataAnnotationsModelMetadataProvider(), null, null, typeof(ObjectId), "Id");

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Id",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            ObjectIdApiBinder binder = new ObjectIdApiBinder();

            HttpActionContext controllerContext = new HttpActionContext();

            // Act
            binder.BindModel(controllerContext, bindingContext);

            Assert.IsNull(bindingContext.Model);
        }

        [Test]
        public void BindModel_NullValue_ReturnsFalse()
        {
            var formCollection = new Dictionary<string, string>
            {
                { "Id", null }
            };

            var valueProvider = new NameValuePairsValueProvider(formCollection, null);
            var modelMetadata = new ModelMetadata(new DataAnnotationsModelMetadataProvider(), null, null, typeof(ObjectId), "Id");

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Id",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            ObjectIdApiBinder binder = new ObjectIdApiBinder();

            HttpActionContext controllerContext = new HttpActionContext();

            // Act
            bool result = binder.BindModel(controllerContext, bindingContext);

            Assert.IsFalse(result);
        }

        [Test]
        public void BindModel_ModelTypeNotObjectId_ReturnsFalse()
        {
            var formCollection = new Dictionary<string, string>
            {
                { "Id", null }
            };

            var valueProvider = new NameValuePairsValueProvider(formCollection, null);
            var modelMetadata = new ModelMetadata(new DataAnnotationsModelMetadataProvider(), null, null, typeof(object), "Id");

            var bindingContext = new ModelBindingContext
            {
                ModelName = "Id",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };

            ObjectIdApiBinder binder = new ObjectIdApiBinder();

            HttpActionContext controllerContext = new HttpActionContext();

            // Act
            bool result = binder.BindModel(controllerContext, bindingContext);

            Assert.IsFalse(result);
        }
    }
}
