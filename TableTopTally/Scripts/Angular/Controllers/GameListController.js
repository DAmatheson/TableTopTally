/* GameDetailController.js
 *  Purpose: Controller for the Game Details partial
 * 
 *  Revision History:
 *      Drew Matheson, 2014.8.3: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/Scripts/Library/Angular/angular-route.js"/>
/// <reference path="~/Scripts/Angular/app.js"/>
/// <reference path="~/Scripts/Angular/Services/GameServices.js"/>

'use strict';

var ttControllers = angular.module('tableTopTally.controllers');

// Controller for the partial gameList.html
ttControllers.controller('GameListController', ['$scope', 'gameDataService',
    function ($scope, gameService)
    {
        // Get all the games
        $scope.games = gameService.query(); 
    }
]);