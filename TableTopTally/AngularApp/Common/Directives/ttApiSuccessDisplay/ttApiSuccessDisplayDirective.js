/* ttApiSuccessDisplayDirective.js
 * Purpose: Directive to display success messages and redirect to a new page
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.25: Created
 */

(function()
{
    'use strict';

    var ttDirectives = angular.module('tableTopTally.directives');

    ttDirectives.directive('ttApiSuccessDisplay', [
        '$timeout', '$interval', '$location', 'tempRedirectionData',
        function($timeout, $interval, $location, tempRedirectionData)
        {
            var controller = function($scope)
            {
                // Controller for the directive

                this.successMessage = ""; // Holds the success message
                this.timeToRedirect = 6; // Counter for the number of seconds until redirect
                this.url = ""; // Holds the url to redirect to

                var redirectTimeoutPromise;
                var countdownPromise;

                this.show = function(message, url, data)
                {
                    // Display the message, redirect to the url in 6 seconds, and setup tempRedirectionData if 
                    // data is included

                    // Cancel any previously running timers in case of quick resubmit
                    if (redirectTimeoutPromise)
                    {
                        $timeout.cancel(redirectTimeoutPromise);
                    }
                    if (countdownPromise)
                    {
                        $interval.cancel(countdownPromise);

                        this.timeToRedirect = 6;
                    }

                    if (!message || !url)
                    {
                        throw "ttApiErrorDisplay.show requires a success message and a URL";
                    }

                    var self = this;

                    this.url = url;
                    this.successMessage = message.trim();

                    if (data !== undefined)
                    {
                        // If data was provided, set tempRedirectionData to data
                        tempRedirectionData.setData(data);
                    }

                    // Redirect after 6 seconds
                    redirectTimeoutPromise = $timeout(function()
                        {
                            self.redirectNow();
                        },
                        6000
                    );

                    // Countdown so time remaining is up to date
                    countdownPromise = $interval(function()
                        {
                            self.timeToRedirect--;
                        },
                        1000, // Delay, 1 second
                        6 // Count, number of times to repeat
                    );
                };

                this.redirectNow = function()
                {
                    $timeout.cancel(redirectTimeoutPromise);
                    $interval.cancel(countdownPromise);

                    $location.url(this.url);
                };

                $scope.$on('$destroy', function()
                {
                    // Cancel the redirection timer when the controller is destroyed
                    $timeout.cancel(redirectTimeoutPromise);
                    $interval.cancel(countdownPromise);
                });
            };

            var link = function(scope, element, attributes, apiSuccessDisplayController)
            {
                // link function for the directive

                // Assign an empty object to scope.tt if it isn't set
                scope.tt = scope.tt || {};

                // Assign the controller to tt.randomPicker
                scope.tt.apiSuccessDisplay = apiSuccessDisplayController;
            };

            return {
                restrict: 'E', // Restrict this directive to elements
                require: 'ttApiSuccessDisplay', // Require this controller for injecting it into the scope
                templateUrl: 'AngularApp/Common/Directives/ttApiSuccessDisplay/ApiSuccessDisplay.html',
                controller: ['$scope', controller], // Inject $scope into controller
                link: link
            };
        }
    ]);
})();