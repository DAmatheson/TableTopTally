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

// Controller for the partial gameDetail.html
ttControllers.controller('GameDetailController', ['$scope', '$routeParams', 'GameDataService',
    function ($scope, $routeParams, gameService)
    {
        $scope.game = gameService.get({ gameId: $routeParams.gameId },
            function (data)
            {
                $scope.name = data.Name;
            }
        );
    }
]);