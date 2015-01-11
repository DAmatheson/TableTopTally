/* lessOrEqualDirective.js
 * Purpose: Angular directive to validate that one field is less than or equal to another
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.14
 */

(function()
{
    'use strict';

    var ttDirectives = angular.module('tableTopTally.directives');

    // Example: <input type="number" name="min" less-or-equal="{{ max }}" />
    //          <input type="number" name="max" />
    //          <span ng-show="form.min.$error.lessOrEqual">Min cannot exceed max</span>

    // Compares if one model value is less than or equal to another and sets error if not
    ttDirectives.directive('lessOrEqual', function()
    {
        var link = function(scope, elm, attrs, ngModelController)
        {
            var validate = function(viewValue)
            {
                var compareToValue = attrs.lessOrEqual;

                var min = parseInt(viewValue, 10);
                var max = parseInt(compareToValue, 10);

                if (angular.equals(max, NaN) || min <= max)
                {
                    ngModelController.$setValidity('lessOrEqual', true);
                }
                else
                {
                    ngModelController.$setValidity('lessOrEqual', false);
                }

                return viewValue;
            };

            ngModelController.$parsers.unshift(validate);

            attrs.$observe('lessOrEqual', function(updatedAttrValue)
            {
                // Revalidate whenever the attribute value is changed
                return validate(ngModelController.$viewValue);
            });
        };

        return {
            restict: 'A',
            require: 'ngModel',
            link: link
        };
    });
})();