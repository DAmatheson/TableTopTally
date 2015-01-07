using System;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using TableTopTally.Attributes;

namespace TableTopTally.Tests.UnitTests.Attributes
{
    public class FakeEntity
    {
        public int Minimum { get; set; }

        public int Maximum { get; set; }

        public string DifferentType { get; set; }

        public NonComparable Uncomparable { get; set; }

        public NonComparable OtherUncomparable { get; set; }
    }

    public struct NonComparable { }

    public class TestingCompareValuesAttribute : CompareValuesAttribute
    {
        public TestingCompareValuesAttribute(string otherProperty, ComparisonCriteria criteria) 
            : base(otherProperty, criteria)
        {
        }

        public ValidationResult CallIsValid(object value, ValidationContext context)
        {
            return IsValid(value, context);
        }
    }

    [TestFixture]
    public class CompareValuesAttributeTests
    {
        private FakeEntity CreateFakeEntity(int min = 0, int max = 0)
        {
            return new FakeEntity
            {
                Minimum = min,
                Maximum = max
            };
        }

        [Test]
        public void Constructor_NullOtherProperty_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CompareValuesAttribute(null, ComparisonCriteria.EqualTo));
        }

        [Test]
        public void CompareValues_InvalidOtherProperty_ThrowsArgumentException()
        {
            FakeEntity entity = CreateFakeEntity();
            ValidationContext validationContext = new ValidationContext(entity) { MemberName = "Minimum" };
            TestingCompareValuesAttribute attribute = new TestingCompareValuesAttribute("InvalidProperty", ComparisonCriteria.EqualTo);

            Assert.Throws<ArgumentException>(() => attribute.CallIsValid(entity.Minimum, validationContext));
        }

        [Test]
        public void CompareValues_DifferentTypedProperties_IsInvalid()
        {
            FakeEntity entity = CreateFakeEntity();
            ValidationContext validationContext = new ValidationContext(entity) { MemberName = "Minimum" };
            TestingCompareValuesAttribute attribute = new TestingCompareValuesAttribute("DifferentType", ComparisonCriteria.EqualTo);

            // Act
            ValidationResult result = attribute.CallIsValid(entity.Minimum, validationContext);

            Assert.That(result.ErrorMessage, Is.StringContaining("types"));
        }

        [Test]
        public void CompareValues_UncomparableProperties_IsInvalid()
        {
            FakeEntity entity = CreateFakeEntity();
            ValidationContext validationContext = new ValidationContext(entity){ MemberName = "Uncomparable" };
            TestingCompareValuesAttribute attribute = new TestingCompareValuesAttribute("OtherUncomparable", ComparisonCriteria.LessThan);

            ValidationResult result = attribute.CallIsValid(entity.Uncomparable, validationContext);

            Assert.That(result.ErrorMessage, Is.StringContaining("IComparable"));
        }

        [TestCase(ComparisonCriteria.EqualTo, 1, 1)]
        [TestCase(ComparisonCriteria.NotEqualTo, 0, 1)]
        [TestCase(ComparisonCriteria.GreaterThan, 1, 0)]
        [TestCase(ComparisonCriteria.GreatThanOrEqualTo, 1, 1)]
        [TestCase(ComparisonCriteria.GreatThanOrEqualTo, 1, 0)]
        [TestCase(ComparisonCriteria.LessThan, 0, 1)]
        [TestCase(ComparisonCriteria.LessThanOrEqualTo, 1, 1)]
        [TestCase(ComparisonCriteria.LessThanOrEqualTo, 0, 1)]
        public void CompareValues_MinimumComparedToMaximumWithValidValues_IsValid(ComparisonCriteria criteria, int minimum, int maximum)
        {
            FakeEntity entity = CreateFakeEntity(minimum, maximum);
            ValidationContext validationContext = new ValidationContext(entity) { MemberName = "Minimum" };
            TestingCompareValuesAttribute attribute = new TestingCompareValuesAttribute("Maximum", criteria);

            // Act
            ValidationResult result = attribute.CallIsValid(entity.Minimum, validationContext);

            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [TestCase(ComparisonCriteria.EqualTo, 1, 0)]
        [TestCase(ComparisonCriteria.NotEqualTo, 1, 1)]
        [TestCase(ComparisonCriteria.GreaterThan, 1, 1)]
        [TestCase(ComparisonCriteria.GreatThanOrEqualTo, 0, 1)]
        [TestCase(ComparisonCriteria.LessThan, 1, 0)]
        [TestCase(ComparisonCriteria.LessThanOrEqualTo, 1, 0)]
        public void CompareValues_MinimumComparedToMaximumWithInvalidValues_IsInvalid(ComparisonCriteria criteria, int minimum, int maximum)
        {
            FakeEntity entity = CreateFakeEntity(minimum, maximum);
            ValidationContext validationContext = new ValidationContext(entity) { MemberName = "Minimum" };
            TestingCompareValuesAttribute attribute = new TestingCompareValuesAttribute("Maximum", criteria);

            // Act
            ValidationResult result = attribute.CallIsValid(entity.Minimum, validationContext);

            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
        }
    }
}
