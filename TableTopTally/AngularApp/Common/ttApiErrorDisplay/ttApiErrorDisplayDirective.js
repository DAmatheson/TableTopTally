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
        var controller = function ()
        {
            // controller for the directive

            this.modelErrors = [];
            this.statusError = "";

            // function to parse model errors
            this.parseModelErrors = function (modelState)
            {
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

            this.parseStatusCode = function (statusCode, actionDescription)
            {
                var error = actionDescription + " failed due to: ";

                switch (statusCode)
                {
                    case 400:
                        error += "Invalid Form Data";
                        break;
                    case 404:
                        error += "Not Found";
                        break;
                    default:
                        error += "Generic Error";
                        break;
                }

                this.statusError = error;
            }

            this.parseResponse = function(httpResponse, actionDescription)
            {
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
            }
        }

        var link = function (scope, element, attributes, apiErrorDisplayController)
        {
            // link function for the directive

            // Assign an empty object to scope.tt if it isn't set
            scope.tt = scope.tt || {};

            // Assign the controller to tt.randomPicker
            scope.tt['apiErrorDisplay'] = apiErrorDisplayController;

            // The promise returned by the timeout in statusError $watch
            var statusErrorDisplayPromise;

            var hideAlert = function()
            {
                // Hide the alert and unfix it from the top
                $("#apiErrorTopAlert").addClass("ng-hide").removeClass("navbar-fixed-top");
            }

            scope.$watch('tt.apiErrorDisplay.statusError', function(newValue)
            {
                if (newValue !== "")
                {
                    // Fix the alert to the top and show it
                    $("#apiErrorTopAlert").addClass("navbar-fixed-top").removeClass("ng-hide");

                    // Create a timeout for removing the alert after 6 seconds
                    statusErrorDisplayPromise = $timeout(hideAlert, 6000);
                }
            });

            // function to cancel the timeout from the $watch on statusError
            var cancelTimeout = function()
            {
                $timeout.cancel(statusErrorDisplayPromise);
            }

            // Cancel the timeout for hiding the alert if the dismiss button is clicked
            $("#btnApiErrorTopAlert").on('click', function()
            {
                cancelTimeout();
            });

            scope.$on('$destroy', function ()
            {
                cancelTimeout(); // Cancel the timeout

                $("#btnApiErrorTopAlert").off('click', cancelTimeout); // Remove the jQuery event watcher

                hideAlert();
            });
        }

        return {
            restrict: 'E', // Restrict this directive to elements
            require: 'ttApiErrorDisplay', // Require this controller for injecting it into the scope
            templateUrl: 'AngularApp/Common/ttApiErrorDisplay/ApiErrorDisplay.html',
            controller: controller,
            link: link
        }
    }
]);