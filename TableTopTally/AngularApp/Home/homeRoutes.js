/* homeRoutes.js
 *  Purpose: Routes for the home module
 */

(function()
{
    'use strict';

    var homeRoutes = angular.module('home.routes', ['ngRoute']);

    homeRoutes.config([
        '$routeProvider',
        function($routeProvider)
        {
            $routeProvider.when('/',
            {
                templateUrl: 'AngularApp/Home/Home.html',
                controller: 'homeController'
            });
        }
    ]);
})();