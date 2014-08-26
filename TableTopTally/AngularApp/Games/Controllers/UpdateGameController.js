/* UpdateGameController.js
 * Purpose: Controller for the game update/editing page in the games module
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.17: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/AngularApp/Games/Services/GameServices.js"/>

'use strict';

var gamesControllers = angular.module('games.controllers');

// Controller for the partial UpdateGame.html
gamesControllers.controller('UpdateGameController',
    ['$scope', '$location', 'gameData', 'gameDataService',

    function ($scope, $location, gameData, gameDataService)
    {
        $scope.masterGame = gameData;

        $scope.game = angular.copy(gameData);

        $scope.formName = "frmEditGame";

        $scope.submitGame = function(game)
        {
            // Submits the game if valid and redirects to the detail page for the game

            if ($scope[$scope.formName].$valid)
            {
                gameDataService.update({ gameId: $scope.masterGame.id }, game,
                    function(data) // Success function
                    {
                        // Display message saying update was successful and redirection is happening
                        $scope.tt.apiSuccessDisplay.show("Update successful.", '/games/' + data.id, data);
                    },
                    function(httpResponse) // Error function
                    {
                        $scope.tt.apiErrorDisplay.parseResponse(httpResponse, "Updating");
                    }
                );
            }
        };
    }
]);