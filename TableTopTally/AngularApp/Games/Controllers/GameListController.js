/* GameListController.js
 *  Purpose: Controller for the game list partial in the games module
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.03: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/AngularApp/Games/Services/GameServices.js"/>

'use strict';

var gamesControllers = angular.module('games.controllers');

// Controller for the partial GameList.html
gamesControllers.controller('GameListController', ['$scope', 'gameList',
    function ($scope, gameList)
    {
        // Get all the games
        $scope.games = gameList;
    }
]);