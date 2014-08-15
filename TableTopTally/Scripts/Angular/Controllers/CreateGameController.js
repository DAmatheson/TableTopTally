/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/Scripts/Library/Angular/angular-route.js"/>
/// <reference path="~/Scripts/Angular/app.js"/>
/// <reference path="~/Scripts/Angular/Services/GameServices.js"/>

'use strict';

var ttControllers = angular.module('tableTopTally.controllers');

// Controller for the partial CreateGame.html
ttControllers.controller('CreateGameController', ['$scope', '$location', 'gameDataService', 'tempRedirectionData',
    function ($scope, $location, gameService, tempRedirectionData)
    {
        $scope.game = {
            name: "",
            minimumPlayers: 0,
            maximumPlayers: 0
        };

        $scope.create = function(game)
        {
            if ($scope.playerCountIsValid(game) && $scope.frmCreateGame.$valid)
            {
                gameService.post(game, function(data)
                {
                    tempRedirectionData.setData(data);

                    $location.url('/games/' + data.id);
                });
            }
        }

        $scope.playerCountIsValid = function (game)
        {
            return game.minimumPlayers <= game.maximumPlayers;
        }
    }
]);