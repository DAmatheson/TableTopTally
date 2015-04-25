/* DeleteGameController.js
 * Purpose: Controller for the game deletion page in the games module
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.18: Created
 */

(function()
{
    'use strict';

    var gamesControllers = angular.module('games.controllers');

    // Controller for the partial DeleteGame.html
    gamesControllers.controller('DeleteGameController',
    [
        '$scope', '$location', 'game', 'gameData', 'layoutValues',
        function($scope, $location, game, gameData, layoutValues)
        {
            $scope.game = game;
            $scope.title = 'Delete ' + $scope.game.name + '?';

            layoutValues.setTitle($scope.title);

            $scope.deleteGame = function()
            {
                // Sends a deletion request. If successful redirect to game list, otherwise retry

                gameData.delete({ gameId: game.id }, // Can't get Id via routeParams so must be done here
                    function() // Success function
                    {
                        $location.url('/games');
                    },
                    function(httpResponse) // Error function
                    {
                        // Parse only status code as deletion doesn't involve modelState
                        $scope.tt.apiErrorDisplay.parseStatusCode(httpResponse.status, 'Deletion');
                    }
                );
            };
        }
    ]);
})();