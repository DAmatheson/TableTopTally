/* DeleteGameController.js
 * Purpose: Controller for the game deletion page in the games module
 * 
 * Revision History:
 *      Drew Matheson, 2014.8.18: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/AngularApp/Games/Services/GameServices.js"/>

'use strict';

var gamesControllers = angular.module('games.controllers');

// Controller for the partial DeleteGame.html
gamesControllers.controller('DeleteGameController',
    ['$scope', '$routeParams', '$location', 'gameData', 'gameDataService', 'apiErrorParser',

    function ($scope, $routeParams, $location, gameData, gameDataService, apiErrorParser)
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
                    // Set up scope in case it hasn't been done
                    $scope.tt = $scope.tt || {};
                    $scope.tt.apiErrors = $scope.tt.apiErrors || {};

                    // Add model errors to scope
                    $scope.tt.apiErrors.statusError = "Deletion failed due to: " +
                        apiErrorParser.parseStatusCode(httpResponse.status);
                }
            );
        };
    }
]);