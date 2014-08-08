/* routes.js
 *  Purpose: Routes for TableTop Tally
 * 
 *  Revision History:
 *      Drew Matheson, 2014.8.2: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/Scripts/Library/Angular/angular-route.js"/>
/// <reference path="~/Scripts/Angular/app.js"/>

'use strict';

var ttRoute = angular.module('tableTopTally.routes');

ttRoute.config(['$routeProvider',
    function ($routeProvider)
    {
        $routeProvider.when('/games',
        {
            templateUrl: 'Static/Partials/GameList.html',
            controller: 'GameListController'
        });

        $routeProvider.when('/games/:gameId',
        {
            templateUrl: 'Static/Partials/GameDetail.html',
            controller: 'GameDetailController'
        });

        $routeProvider.otherwise(
        {
            templateUrl: 'Static/Partials/Home.html',
            redirectTo: 'index'
        });
    }
]);