/* ObjectIdModelValidatorTest.cs
 * 
 * Purpose: Unit tests for the ObjectIdModelValidator class
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.20: Created
*/

using MongoDB.Bson;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using TableTopTally.Helpers;

namespace TableTopTally.Tests.Helpers
{
    [TestFixture]
    public class ObjectIdModelValidatorTest
    {
        [Test(Description = "Test the ObjectIdModelValidator with a valid ObjectId")]
        public void ValidObjectId()
        {
            // Arrange
            var validObjectId = ObjectId.GenerateNewId();

            // Act
            var result = ObjectIdModelValidator.IsValid(validObjectId);

            // Assert
            Assert.IsNull(result);
            Assert.AreSame(ValidationResult.Success, result);
        }

        [Test(Description = "Test the ObjectIdModelValidator with an invalid ObjectId")]
        public void InvalidObjectId()
        {
            // Arrange
            var invalidObjectId = ObjectId.Empty;

            // Act
            var result = ObjectIdModelValidator.IsValid(invalidObjectId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ValidationResult>(result);
            Assert.AreEqual("The Id must be a valid ObjectId.", result.ErrorMessage);
        }
    }
}