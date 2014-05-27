using System.Web.Mvc;
using MongoDB.Bson;

namespace TableTopTally.Helpers
{

    /// <summary>
    /// A model binder for mongoDB objectIds
    /// </summary>
    public class ObjectIdBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            return new ObjectId(result.AttemptedValue);
        }
    }
}