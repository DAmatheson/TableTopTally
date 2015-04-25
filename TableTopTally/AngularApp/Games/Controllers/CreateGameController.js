/* CreateGameController.js
 * Purpose: Controller for the game creation page in the games module
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.15: Created
 */

(function()
{
    'use strict';

    var gamesControllers = angular.module('games.controllers');

    // Controller for the partial CreateGame.html
    gamesControllers.controller('CreateGameController',
    [
        '$scope', '$location', 'gameData', 'layoutValues',
        function($scope, $location, gameData, layoutValues)
        {
            $scope.title = 'Create a Game';

            layoutValues.setTitle($scope.title);

            $scope.formName = 'frmGame';

            $scope.submitGame = function(game)
            {
                // submits the game if valid, and redirects to the detail page for the game

                if ($scope[$scope.formName].$valid)
                {
                    console.log(game);
                    gameData.create(game,
                        function(data) // Success function
                        {
                            $location.url('/games/' + data.url);
                        },
                        function(httpResponse) // Error function
                        {
                            $scope.tt.apiErrorDisplay.parseResponse(httpResponse, 'Creation');
                        }
                    );
                }
            };
        }
    ]);
})();