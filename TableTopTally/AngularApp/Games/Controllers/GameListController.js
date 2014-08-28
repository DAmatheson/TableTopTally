/* GameListController.js
 *  Purpose: Controller for the game list partial in the games module
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.03: Created
 */

(function()
{
    'use strict';

    var gamesControllers = angular.module('games.controllers');

    // Controller for the partial GameList.html
    gamesControllers.controller('GameListController', [
        '$scope', 'gameList', 'layoutValues',
        function($scope, gameList, layoutValues)
        {
            $scope.games = gameList;
            $scope.title = 'Games List';

            layoutValues.setTitle($scope.title);
        }
    ]);
})();