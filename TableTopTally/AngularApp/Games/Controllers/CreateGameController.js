/* CreateGameController.js
 * Purpose: Controller for the game creation page in the games module
 * 
 * Revision History:
 *      Drew Matheson, 2014.8.15: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/AngularApp/Games/Services/GameServices.js"/>

'use strict';

var gamesControllers = angular.module('games.controllers');

// Controller for the partial CreateGame.html
gamesControllers.controller('CreateGameController',
    ['$scope', '$location', 'gameDataService', 'tempRedirectionData', 'apiErrorParser',

    function ($scope, $location, gameService, tempRedirectionData, apiErrorParser)
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
                        // Display message showing creation was successful and redirection is happening

                        tempRedirectionData.setData(data);

                        $location.url('/games/' + data.id);
                    },
                    function (httpResponse) // Error function
                    {
                        // Set up scope in case it hasn't been done
                        $scope.tt = $scope.tt || {};
                        $scope.tt.apiErrors = $scope.tt.apiErrors || {};

                        // Add model errors to scope
                        $scope.tt.apiErrors.modelErrors = apiErrorParser.parseModelErrors(httpResponse);
                        $scope.tt.apiErrors.statusError = "Creation failed due to: " +
                            apiErrorParser.parseStatusCode(httpResponse.status);
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