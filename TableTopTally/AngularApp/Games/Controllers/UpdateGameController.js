/* UpdateGameController.js
 * Purpose: Controller for the game update/editing page in the games module
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.17: Created
 */

(function()
{
    'use strict';

    var gamesControllers = angular.module('games.controllers');

    // Controller for the partial UpdateGame.html
    gamesControllers.controller('UpdateGameController', [
        '$scope', '$location', 'game', 'gameData', 'layoutValues',
        function($scope, $location, game, gameData, layoutValues)
        {
            $scope.masterGame = game;
            $scope.game = angular.copy(game);

            $scope.formName = "frmEditGame";
            $scope.title = 'Editing ' + $scope.masterGame.name;

            layoutValues.setTitle($scope.title);

            $scope.submitGame = function(updatedGame)
            {
                // Submits the game if valid and redirects to the detail page for the game

                if ($scope[$scope.formName].$valid)
                {
                    gameData.update({ gameId: $scope.masterGame.id }, updatedGame,
                        function(data) // Success function
                        {
                            // Display message saying update was successful and redirection is happening
                            $scope.tt.apiSuccessDisplay.show('Update successful.', '/games/' + data.id, data);
                        },
                        function(httpResponse) // Error function
                        {
                            $scope.tt.apiErrorDisplay.parseResponse(httpResponse, 'Updating');
                        }
                    );
                }
            };
        }
    ]);
})();