/* ObjectIdMvcBinder.cs
 * 
 * Purpose: A model binder for ObjectIds
 * 
 * Revision History:
 *      Drew Matheson, 2014.05.29: Created
 */

using System.Web.Mvc;
using MongoDB.Bson;

namespace TableTopTally.Binders
{
    /// <summary>
    /// A model binder for mongoDB objectIds
    /// </summary>
    public class ObjectIdMvcBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            ObjectId result; 
            ObjectId.TryParse(value.AttemptedValue, out result);

            return result;
        }
    }
}