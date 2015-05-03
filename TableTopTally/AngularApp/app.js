/* app.js
 *  Purpose: Angular app for TableTop Tally
*/

(function()
{
    'use strict';

    // Declare main app module which depends on filters, services, directives, and controllers
    var ttApp = angular.module('tableTopTally', [
        'layout',
        'home',
        'games',
        'notFound.controllers',
        'tableTopTally.services',
        'tableTopTally.directives',
        'angular-loading-bar'
    ]);

    ttApp.config([
        '$locationProvider', '$routeProvider', 'cfpLoadingBarProvider',
        function ($locationProvider, $routeProvider, cfpLoadingBarProvider)
        {
            // 404 page with link back to '/'
            $routeProvider.otherwise(
            {
                templateUrl: 'AngularApp/NotFound/404.html',
                controller: 'notFoundController'
            });

            // use HTML5 History API (Removes /#/ after domain but requires proper handling on the server)
            $locationProvider.html5Mode(true);

            cfpLoadingBarProvider.includeBar = false;
        }
    ]);

    // Define common modules here rather than in their own files so that they don't override each other
    // or require a specific loading order to prevent overriding
    angular.module('tableTopTally.services', []);

    // ttAppVersionDirective and ttRandomPicker require ValuesService
    angular.module('tableTopTally.directives', ['tableTopTally.services']); 
})();