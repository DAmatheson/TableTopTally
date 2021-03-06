﻿/* ObjectIdApiBinder.cs
 * Purpose: Web API 2 binder for ObjectId
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.27: Created
 */

using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
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

            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value == null || value.AttemptedValue == null || value.RawValue == null)
            {
                return false;
            }

            ObjectId parsedValue;

            if (!ObjectId.TryParse(value.AttemptedValue, out parsedValue) || parsedValue == ObjectId.Empty)
            {
                return false;
            }

            bindingContext.Model = parsedValue;

            return true;
        }
    }
}