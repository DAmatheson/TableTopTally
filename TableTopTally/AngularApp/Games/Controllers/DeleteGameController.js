/* DeleteGameController.js
 * Purpose: Controller for the game deletion page in the games module
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.18: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/AngularApp/Games/Services/GameServices.js"/>

'use strict';

var gamesControllers = angular.module('games.controllers');

// Controller for the partial DeleteGame.html
gamesControllers.controller('DeleteGameController',
    ['$scope', '$routeParams', '$location', 'gameData', 'gameDataService',

    function ($scope, $routeParams, $location, gameData, gameDataService)
    {
        $scope.game = gameData;

        $scope.retryCount = 0;

        $scope.deleteGame = function()
        {
            // Sends a deletion request. If successful redirect to game list, otherwise retry

            gameDataService.delete({ gameId: $routeParams.gameId },
                function(value, responseHeaders) // Success function
                {
                    // Display message saying deletion succeeded and redirection is happening

                    $location.url('/games/');
                },
                function (httpResponse) // Error function
                { 
                    $scope.tt.apiErrorDisplay.parseStatusCode(httpResponse.status, "Deletion");
                }
            );
        };
    }
]);