/* ttApiErrorDisplayDirective.js
 * Purpose: Directive to parse and display errors returned from the ASP.NET Web API
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.25: Created
 */

'use strict';

var ttDirectives = angular.module('tableTopTally.directives');

ttDirectives.directive('ttApiErrorDisplay', ['$timeout',
    function ($timeout)
    {
        var controller = function($scope)
        {
            // Controller for the directive

            this.modelErrors = []; // Holds the model error messages
            this.statusError = ""; // Holds the status error message
            this.statusIsNew = false; // Flag used to show/hide the status alert

            var statusTimeoutPromise;

            this.parseModelErrors = function(modelState)
            {
                // Parser model errors and add them to the modelErrors property

                var errors = [];

                for (var key in modelState)
                {
                    if (modelState.hasOwnProperty(key))
                    {
                        for (var i = 0; i < modelState[key].length; i++)
                        {
                            errors.push(modelState[key][i]);
                        }
                    }
                }

                this.modelErrors = errors;
            };

            this.parseStatusCode = function(statusCode, actionDescription)
            {
                // Parses statusCode into a message, sets statusIsNew to true

                var error = actionDescription + " failed due to ";

                switch (statusCode)
                {
                    case 400:
                        error += "invalid form data";
                        break;
                    case 404:
                        error += "a Not Found error";
                        break;
                    default:
                        error += "a generic error";
                        break;
                }

                this.statusError = error;

                this.setStatusIsNew(true);
            };

            this.parseResponse = function(httpResponse, actionDescription)
            {
                // Parse both the status and model errors

                if (!httpResponse)
                {
                    throw "httpResponse was not passed to ttApiErrorParser's parseResponse function";
                }

                if (httpResponse.data && httpResponse.data.modelState)
                {
                    this.parseModelErrors(httpResponse.data.modelState);
                }

                if (httpResponse.status)
                {
                    actionDescription = actionDescription.trim() || "The action";

                    this.parseStatusCode(httpResponse.status, actionDescription);
                }
            };

            this.setStatusIsNew = function(isNew)
            {
                // Set the statusIsNew flag to isNew

                if (isNew === true)
                {
                    this.statusIsNew = isNew;

                    var self = this; // Required to have access to 'this' within the $timeout

                    // Set a timeout to change statusIsNew to false so the alert goes away
                    statusTimeoutPromise = $timeout(function()
                        {
                            self.setStatusIsNew(false);
                        },
                        6000
                    );
                }
                else if (isNew === false)
                {
                    this.statusIsNew = isNew;

                    // Cancel the timeout in case the swap was called before the timer ended
                    $timeout.cancel(statusTimeoutPromise);
                }
            };

            $scope.$on('$destroy', function()
            {
                // Cancel the new status timer when the controller is destroyed
                $timeout.cancel(statusTimeoutPromise);
            });
        };

        var link = function(scope, element, attributes, apiErrorDisplayController)
        {
            // link function for the directive

            // Assign an empty object to scope.tt if it isn't set
            scope.tt = scope.tt || {};

            // Assign the controller to tt.randomPicker
            scope.tt.apiErrorDisplay = apiErrorDisplayController;
        };

        return {
            restrict: 'E', // Restrict this directive to elements
            require: 'ttApiErrorDisplay', // Require this controller for injecting it into the scope
            templateUrl: 'AngularApp/Common/ttApiErrorDisplay/ApiErrorDisplay.html',
            controller: controller,
            link: link
        };
    }
]);