/* app.js
 *  Purpose: Angular app for TableTop Tally
 *
 *  Revision History:
 *      Drew Matheson, 2014.8.3: Created
*/

'use strict';

// Declare app level module which depends on filters, services, directives, and controllers
var ttApp = angular.module('tableTopTally', [
    'tableTopTally.routes',
    //'tableTopTally.filters',
    'tableTopTally.services',
    'tableTopTally.directives',
    'tableTopTally.controllers'
]);

ttApp.config(['$locationProvider',
    function ($locationProvider)
    {
        // use HTML5 History API (Removes /#/ after domain but requires proper handling on the server)
        $locationProvider.html5Mode(true);
    }
]);


// Define common modules here rather than in their own files so that they don't override each other
// or require a specific loading order to prevent overriding
angular.module('tableTopTally.controllers', []);
angular.module('tableTopTally.services', ['ngResource']); // ttt.services requires ngResource
angular.module('tableTopTally.routes', ['ngRoute']); // ttt.routes requires ngRoute
angular.module('tableTopTally.directives', []);