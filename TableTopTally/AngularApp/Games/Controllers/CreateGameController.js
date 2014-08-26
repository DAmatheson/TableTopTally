/* CreateGameController.js
 * Purpose: Controller for the game creation page in the games module
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.15: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/AngularApp/Games/Services/GameServices.js"/>

'use strict';

var gamesControllers = angular.module('games.controllers');

// Controller for the partial CreateGame.html
gamesControllers.controller('CreateGameController',
    ['$scope', '$location', 'gameDataService',

    function ($scope, $location, gameService)
    {
        $scope.formName = "frmGame";

        $scope.submitGame = function(game)
        {
            // submits the game if valid, and redirects to the detail page for the game

            if ($scope.playerCountIsValid(game) && $scope[$scope.formName].$valid)
            {
                gameService.create(game,
                    function(data) // Success function
                    {
                        // Display message saying update was successful and redirection is happening
                        $scope.tt.apiSuccessDisplay.show("Creation successful.", '/games/' + data.id, data);
                    },
                    function (httpResponse) // Error function
                    {
                        $scope.tt.apiErrorDisplay.parseResponse(httpResponse, "Creation");
                    }
                );
            }
        };

        $scope.playerCountIsValid = function(game)
        {
            // Validates that the min players is less than or equal to max players

            // game is included to prevent accessing its properties if it isn't passed in
            return game && game.minimumPlayers <= game.maximumPlayers;
        };
    }
]);