﻿/* ObjectIdMvcBinder.cs
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
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            return new ObjectId(result.AttemptedValue);
        }
    }
}