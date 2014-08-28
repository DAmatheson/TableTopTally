/* ObjectIdApiBinder.cs
 * Purpose: Web API 2 binder for ObjectId
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.27: Created
 */

using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using MongoDB.Bson;

namespace TableTopTally.Binders
{
    public class ObjectIdApiBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof (ObjectId))
            {
                return false;
            }

            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (result == null)
            {
                return false;
            }

            ObjectId parsedId;

            if (!ObjectId.TryParse(result.AttemptedValue, out parsedId) || parsedId == ObjectId.Empty)
            {
                return false;
            }

            bindingContext.Model = parsedId;

            return true;
        }
    }
}