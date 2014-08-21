/* UpdateGameController.js
 * Purpose: Controller for the game update/editing page in the games module
 * 
 * Revision History:
 *      Drew Matheson, 2014.8.17: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/AngularApp/Games/Services/GameServices.js"/>

'use strict';

var gamesControllers = angular.module('games.controllers');

// Controller for the partial UpdateGame.html
gamesControllers.controller('UpdateGameController',
    ['$scope', '$routeParams', '$location', 'gameData', 'gameDataService', 'tempRedirectionData',

    function ($scope, $routeParams, $location, gameData, gameDataService, tempRedirectionData)
    {
        $scope.masterGame = gameData;

        $scope.game = angular.copy(gameData);

        $scope.formName = "frmEditGame";

        $scope.submitGame = function(game)
        {
            // submits the game if valid and redirects to the detail page for the game

            game.id = "blah";

            if ($scope.playerCountIsValid(game) && $scope[$scope.formName].$valid)
            {
                gameDataService.update({ gameId: $scope.masterGame.id }, game,
                    function (data, responseHeaders) // Success function
                    {
                        tempRedirectionData.setData(data);

                        $location.url('/games/' + data.id);
                    },
                    function (httpResponse) // Error function
                    {
                        //console.log(httpResponse);
                    }
                );
            }
        }

        $scope.playerCountIsValid = function (game)
        {
            // Validates that the min players is less than or equal to max players

            // game is included to prevent accessing its properties if it isn't passed in
            return game && game.minimumPlayers <= game.maximumPlayers;
        }
    }
]);