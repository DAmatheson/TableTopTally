/* FromUriOnGetActionValueBinder.cs
 * Purpose: ActionValueBinder that treats GET action parameters as if they had the [FromUri] attribute
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.28: Created
 */ 

using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace TableTopTally.Binders
{
    /// <summary>
    /// Binds GET requests as if the parameters had the [FromUri] attribute
    /// </summary>
    public class FromUriOnGetActionValueBinder : DefaultActionValueBinder
    {
        protected override HttpParameterBinding GetParameterBinding(HttpParameterDescriptor parameter)
        {
            return parameter.ActionDescriptor.SupportedHttpMethods.Contains(HttpMethod.Get)
                 ? parameter.BindWithAttribute(new FromUriAttribute())
                 : base.GetParameterBinding(parameter);
        }
    }
}