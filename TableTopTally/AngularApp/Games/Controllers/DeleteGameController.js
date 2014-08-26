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
                function() // Success function
                {
                    // Display message saying update was successful and redirection is happening
                    $scope.tt.apiSuccessDisplay.show("Removal successful.", '/games/');
                },
                function (httpResponse) // Error function
                {
                    // Parse only status code as deletion doesn't involve modelState
                    $scope.tt.apiErrorDisplay.parseStatusCode(httpResponse.status, "Deletion");
                }
            );
        };
    }
]);