/* ObjectIdModelValidatorTest.cs
 * 
 * Purpose: Unit tests for the ObjectIdModelValidator class
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.20: Created
*/

using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.Helpers;

namespace TableTopTally.Tests.UnitTests.Helpers
{
    [TestFixture]
    public class ObjectIdModelValidatorTest
    {
        [Test(Description = "Test the ObjectIdModelValidator with a valid ObjectId")]
        public void ValidObjectId()
        {
            // Arrange
            var validObjectId = new ObjectId("54a07c8a4a91a323e83d78d2");

            // Act
            ValidationResult result = ObjectIdModelValidator.IsValid(validObjectId);

            // Assert
            Assert.AreSame(ValidationResult.Success, result);
        }

        [Test(Description = "Test the ObjectIdModelValidator with an invalid ObjectId")]
        public void InvalidObjectId()
        {
            // Arrange
            var invalidObjectId = ObjectId.Empty;

            // Act
            ValidationResult result = ObjectIdModelValidator.IsValid(invalidObjectId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.ErrorMessage, Is.EqualTo("The Id must be a valid ObjectId."));
        }
    }
}