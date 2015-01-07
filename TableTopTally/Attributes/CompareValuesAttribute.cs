using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace TableTopTally.Attributes
{
    // Note: Slightly modified version of 
    // http://cncrrnt.com/blog/index.php/2011/01/custom-validationattribute-for-comparing-properties/

    /// <summary>
    /// Specifies that the field must compare favourably with the named field. If objects to check are 
    /// not the same type, false will be return
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property |
        AttributeTargets.Field |
        AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class CompareValuesAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// The other property to compare to
        /// </summary>
        public string OtherProperty { get; set; }

        /// <summary>
        /// The comparison criteria used for this instance
        /// </summary>
        public ComparisonCriteria Criteria { get; set; }

        /// <summary>
        /// Creates the attribute
        /// </summary>
        /// <param name="otherProperty">The other property to compare to</param>
        /// <param name="criteria">The <see cref="ComparisonCriteria"/> to use when comparing</param>
        public CompareValuesAttribute(string otherProperty, ComparisonCriteria criteria)
        {
            if (otherProperty == null)
                throw new ArgumentNullException("otherProperty");

            OtherProperty = otherProperty;
            Criteria = criteria;
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid. For this to be the case, 
        /// the objects must be of the same type and satisfy the comparison criteria. Null values will 
        /// return false in all cases except when both objects are null. The objects will need to 
        /// implement IComparable for the GreaterThan, LessThan, GreatThanOrEqualTo, and LessThanOrEqualTo
        /// Enum values
        /// </summary>
        /// <param name="value">The value of the object to validate</param>
        /// <param name="validationContext">The validation context</param>
        /// <returns>A validation result if the object is invalid, null if the object is valid</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if otherProperty can't be found or
        ///     the properties aren't the same type
        /// </exception>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // The other property
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);

            if (otherPropertyInfo == null)
            {
                throw new InvalidOperationException(string.Format("The {0} property couldn't be found. " +
                    "The otherProperty value supplied to the constructor must be the name of a property.",
                    OtherProperty));
            }

            // Check types
            if (validationContext.ObjectType.GetProperty(validationContext.MemberName).PropertyType !=
                otherPropertyInfo.PropertyType)
            {
                throw new InvalidOperationException(String.Format(
                    "The types of the properties {0} and {1} must be the same.",
                    validationContext.DisplayName,
                    OtherProperty));
            }

            // Get the other value
            var other = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            // Equals to comparison
            if (Criteria == ComparisonCriteria.EqualTo)
            {
                if (Equals(value, other))
                {
                    return ValidationResult.Success;
                }
            }
            else if (Criteria == ComparisonCriteria.NotEqualTo)
            {
                if (!Equals(value, other))
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                // Check that both objects implement IComparable
                // Note: Both must be the same type to get here, so only one check is required
                if (!(value is IComparable))
                {
                    return
                        new ValidationResult(
                            String.Format(
                                "{0} and {1} must both implement IComparable",
                                validationContext.DisplayName, OtherProperty));
                }

                // Compare the objects
                var result = Comparer.Default.Compare(value, other);

                if (Criteria == ComparisonCriteria.GreaterThan && result > 0)
                {
                    return ValidationResult.Success;
                }

                if (Criteria == ComparisonCriteria.LessThan && result < 0)
                {
                    return ValidationResult.Success;
                }

                if (Criteria == ComparisonCriteria.GreatThanOrEqualTo && result >= 0)
                {
                    return ValidationResult.Success;
                }

                if (Criteria == ComparisonCriteria.LessThanOrEqualTo && result <= 0)
                {
                    return ValidationResult.Success;
                }
            }

            // Got this far must mean the items don't meet the comparison criteria
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        /// <summary>
        /// Applies formatting to an error message.
        /// </summary>
        /// <param name="name">The name to include in the error message</param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            // Get the description of the ComparisonCriteria enum value
            var description = (DescriptionAttribute)TypeDescriptor.GetProperties(this)["Criteria"].
                Attributes[typeof(DescriptionAttribute)];

            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name,
                OtherProperty, description.Description);
        }
    }

    /// <summary>
    /// Indicates a comparison criteria used by the ComparisonCriteria attribute
    /// </summary>
    public enum ComparisonCriteria
    {
        /// <summary>
        /// Check if the values are equal
        /// </summary>
        [Description("=")]
        EqualTo,

        /// <summary>
        /// Check if the values are not equal
        /// </summary>
        [Description("!=")]
        NotEqualTo,

        /// <summary>
        /// Check if this value is greater than the supplied value
        /// </summary>
        [Description(">")]
        GreaterThan,

        /// <summary>
        /// Check if this value is less than the supplied value
        /// </summary>
        [Description("<")]
        LessThan,

        /// <summary>
        /// Check if this value is greater than or equal to the supplied value
        /// </summary>
        [Description(">=")]
        GreatThanOrEqualTo,

        /// <summary>
        /// Check if this value is less than or equal to the supplied value
        /// </summary>
        [Description("<=")]
        LessThanOrEqualTo
    }
}