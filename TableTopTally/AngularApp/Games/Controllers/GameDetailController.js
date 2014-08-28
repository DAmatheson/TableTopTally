/* GameDetailController.js
 *  Purpose: Controller for the game details partial in the games module
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.03: Created
 */

(function()
{
    'use strict';

    var gamesControllers = angular.module('games.controllers');

    // Controller for the partial GameDetail.html
    gamesControllers.controller('GameDetailController', [
        '$scope', 'game', 'layoutValues',
        function($scope, game, layoutValues)
        {
            $scope.game = game;
            $scope.title = $scope.game.name;

            layoutValues.setTitle($scope.title);
        }
    ]);
})();