/* app.js
 *  Purpose: Angular app for TableTop Tally
 *
 *  Revision History:
 *      Drew Matheson, 2014.8.3: Created
*/

'use strict';

// Declare main app module which depends on filters, services, directives, and controllers
var ttApp = angular.module('tableTopTally', [
    'home',
    'games',
    'tableTopTally.services',
    'tableTopTally.directives'
]);

ttApp.config(['$locationProvider', '$routeProvider',
    function ($locationProvider, $routeProvider)
    {
        // 404 page with link back to '/'
        $routeProvider.otherwise(
        {
            templateUrl: 'AngularApp/404.html'
        });

        // use HTML5 History API (Removes /#/ after domain but requires proper handling on the server)
        $locationProvider.html5Mode(true);
    }
]);

// Define common modules here rather than in their own files so that they don't override each other
// or require a specific loading order to prevent overriding
angular.module('tableTopTally.services', ['ngResource']); // tt.services requires ngResource
angular.module('tableTopTally.directives', []);