﻿using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
        /// <summary>
        /// The other property to compare to
        /// </summary>
        public string OtherProperty { get; set; }

        /// <summary>
        /// The comparison criteria used for this instance
        /// </summary>
        public CompareValues Criteria { get; set; }

        /// <summary>
        /// Creates the attribute
        /// </summary>
        /// <param name="otherProperty">The other property to compare to</param>
        /// <param name="criteria">The CompareValues Enum comparison criteria</param>
        public CompareValuesAttribute(string otherProperty, CompareValues criteria)
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
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // The other property
            var property = validationContext.ObjectType.GetProperty(OtherProperty);

            // Check it is not null
            if (property == null)
            {
                return new ValidationResult(String.Format("Unknown property: {0}.", OtherProperty));
            }

            // Check types
            if (validationContext.ObjectType.GetProperty(validationContext.MemberName).PropertyType !=
                property.PropertyType)
            {
                return
                    new ValidationResult(
                        String.Format(
                            "The types of {0} and {1} must be the same.", validationContext.DisplayName,
                            OtherProperty));
            }

            // Get the other value
            var other = property.GetValue(validationContext.ObjectInstance, null);

            // Equals to comparison
            if (Criteria == CompareValues.EqualTo)
            {
                if (Equals(value, other))
                {
                    return ValidationResult.Success;
                }
            }
            else if (Criteria == CompareValues.NotEqualTo)
            {
                if (!Equals(value, other))
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                // Check that both objects implement IComparable
                if (!(value is IComparable) || !(other is IComparable))
                {
                    return
                        new ValidationResult(
                            String.Format(
                                "{0} and {1} must both implement IComparable",
                                validationContext.DisplayName, OtherProperty));
                }

                // Compare the objects
                var result = Comparer.Default.Compare(value, other);

                if (Criteria == CompareValues.GreaterThan && result > 0)
                {
                    return ValidationResult.Success;
                }

                if (Criteria == CompareValues.LessThan && result < 0)
                {
                    return ValidationResult.Success;
                }

                if (Criteria == CompareValues.GreatThanOrEqualTo && result >= 0)
                {
                    return ValidationResult.Success;
                }

                if (Criteria == CompareValues.LessThanOrEqualTo && result <= 0)
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
            var description = (DescriptionAttribute)TypeDescriptor.GetProperties(this)["Criteria"].
                Attributes[typeof(DescriptionAttribute)];

            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name,
                OtherProperty, description);
        }
    }

    /// <summary>
    /// Indicates a comparison criteria used by the CompareValues attribute
    /// </summary>
    public enum CompareValues
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