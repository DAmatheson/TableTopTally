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
        $routeProvider.when("/",
        {
            templateUrl: 'Static/Partials/Home.html',
        }).
        when('/games',
        {
            templateUrl: 'Static/Partials/GameList.html',
            controller: 'GameListController'
        }).
        when('/games/create',
        {
            templateUrl: 'Static/Partials/CreateGame.html',
            controller: 'CreateGameController'
        }).
        when('/games/:gameId', // Anything /games/* must come before this, or it is seen as :gameId 
        {
            templateUrl: 'Static/Partials/GameDetail.html',
            controller: 'GameDetailController'
        }).
        when('/Account/Register',
        {
            templateUrl: '/Account/Register?angular=true'
        });

        // 404 page with link back to '/'
        $routeProvider.otherwise(
        {
            templateUrl: 'Static/Partials/404.html'
        });
    }
]);