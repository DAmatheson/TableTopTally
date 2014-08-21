/* ObjectIdModelValidatorTest.cs
 * 
 * Purpose: Unit tests for the ObjectIdModelValidator class
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.20: Created
*/ 

using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using TableTopTally.Helpers;

namespace TableTopTally.Tests.Helpers
{
    [TestClass]
    public class ObjectIdModelValidatorTest
    {
        [TestMethod]
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

        [TestMethod]
        public void InvalidObjectId()
        {
            // Arrange
            var invalidObjectId = ObjectId.Empty;

            // Act
            var result = ObjectIdModelValidator.IsValid(invalidObjectId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof (ValidationResult));
            Assert.AreEqual(result.ErrorMessage, "The Id must be a valid ObjectId.");
        }
    }
}