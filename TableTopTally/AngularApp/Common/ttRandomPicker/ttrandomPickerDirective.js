/* ttRandomPickerDirective.js
 * Purpose: Directive to randomly pick a value from text input
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.21: Created
 */

'use strict';

var ttDirectives = angular.module('tableTopTally.directives');

ttDirectives.directive('ttRandomPicker', 
    function() 
    {
        var controller = function (dateFilter, randomNames)
        {
            // controller for the directive

            this.randomResult = "";

            this.randomValues = randomNames; // intialize the value

            this.pickValue = function pickValue()
            {
                var output = randomValue(this.randomValues) + " " + dateFilter(new Date, "MM/dd @ h:mm:ss a" );

                this.randomResult = output;
            }

            function randomValue(randomValues)
            {
                var reValueSplit = /[,?\s*]+/; // 0-1 commas, any # spaces, one or more repetitions
                var reStartTrim = /^[,?\s*]+/; // At the start, 0-1 commas, any # spaces, one or more repetitions
                var reEndTrim = /[,?\s*]+$/; // At the end, 0-1 commas, any # spaces, one or more repetitions

                var trimmedNames = randomValues.replace(reStartTrim, '').replace(reEndTrim, '');

                var splitValues = trimmedNames.split(reValueSplit);

                var randomNumber = getRandomInt(0, splitValues.length - 1);

                var pickedValue = splitValues[randomNumber];

                return pickedValue;
            }

            function getRandomInt(min, max)
            {
                return Math.floor(Math.random() * (max - min + 1)) + min;
            }
        }

        var link = function (scope, element, attributes, randomPickerController)
        {
            // link function for the directive

            // Assign an empty object to scope.tt if it isn't set
            scope.tt = scope.tt || {};

            // Assign the controller to tt.randomPicker
            scope.tt['randomPicker'] = randomPickerController;
        }

        return {
            restrict: 'E', // Restrict this directive to elements
            require: 'ttRandomPicker', // Require this controller for injecting it into the scope
            templateUrl: 'AngularApp/Common/ttRandomPicker/RandomPicker.html',
            controller: ['dateFilter', 'randomNames',  controller],
            link: link
        }
    }
);