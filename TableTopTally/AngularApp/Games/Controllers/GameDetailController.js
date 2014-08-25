/* GameDetailController.js
 *  Purpose: Controller for the game details partial in the games module
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.03: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/AngularApp/Games/Services/GameServices.js"/>

'use strict';

var gamesControllers = angular.module('games.controllers');

// Controller for the partial GameDetail.html
gamesControllers.controller('GameDetailController', ['$scope', 'gameData',
    function ($scope, gameData)
    {
        $scope.game = gameData;
    }
]);