/* gamesRoutes.js
 *  Purpose: Routes for the games module
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.02: Created
 *      Drew Matheson, 2014.08.19: Added resolve property to whens so page isn't loaded until data returns
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/Scripts/Library/Angular/angular-route.js"/>
/// <reference path="~/AngularApp/Games/games.js"/>

'use strict';

var gamesRoute = angular.module('games.routes', ['ngRoute']);

gamesRoute.config(['$routeProvider',
    function ($routeProvider)
    {
        $routeProvider.
        when('/games',
        {
            templateUrl: 'AngularApp/Games/Partials/GameList.html',
            controller: 'GameListController',
            resolve:
            {
                gameList: ['gameDataService',
                    function (gameDataService)
                    {
                        return gameDataService.query().$promise;
                    }
                ]
            }
        }).
        when('/games/create',
        {
            templateUrl: 'AngularApp/Games/Partials/CreateGame.html',
            controller: 'CreateGameController'
        }).
        when('/games/update/:gameId', // Anything /games/update/* must come before this
        {
            templateUrl: 'AngularApp/Games/Partials/UpdateGame.html',
            controller: 'UpdateGameController',
            resolve:
            {
                gameData: ['$route', 'gameDataService',
                    function ($route, gameDataService)
                    {
                        return gameDataService.get({ gameId: $route.current.params.gameId }).$promise;
                    }
                ]
            }
        }).
        when('/games/delete/:gameId', // Anything /games/delete/* must come before this
        {
            templateUrl: 'AngularApp/Games/Partials/DeleteGame.html',
            controller: 'DeleteGameController',
            resolve:
            {
                gameData: ['$route', 'gameDataService',
                    function($route, gameDataService)
                    {
                        return gameDataService.get({ gameId: $route.current.params.gameId }).$promise;
                    }
                ]
            }
        }).
        when('/games/:gameId', // Anything /games/* must come before this, or it is seen as :gameId 
        {
            templateUrl: 'AngularApp/Games/Partials/GameDetails.html',
            controller: 'GameDetailController',
            resolve:
            {
                gameData: ['$route', 'gameDataService', 'tempRedirectionData',
                    function($route, gameDataService, tempRedirectionData)
                    {
                        var data = tempRedirectionData.getData();

                        if (data === null)
                        {
                            data = gameDataService.get({ gameId: $route.current.params.gameId }).$promise;
                        }

                        return data;
                    }
                ]
            }
        });
    }
]);