/* ObjectIdModelValidator.cs
 * Purpose: ObjectId validator for use with the CustomValidationAttribute
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.20: Created
 */

using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace TableTopTally.DataModels.Validation
{
    /// <summary>
    /// Provides an IsValid method for validating that a property of type ObjectId is valid when model
    /// validation occurs
    /// </summary>
    public class ObjectIdModelValidator
    {
        /// <summary>
        /// Validates that an ObjectId is not equal to its default value
        /// </summary>
        /// <param name="id">The ObjectId to validate</param>
        /// <returns>ValidationResult indicating the validity of the id</returns>
        public static ValidationResult IsValid(ObjectId id)
        {
            if (id == ObjectId.Empty)
            {
                return new ValidationResult("The Id must be a valid ObjectId.");
            }

            return ValidationResult.Success;
        }
    }
}