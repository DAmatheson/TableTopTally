/* GameServices.js
 *  Purpose: Service module for getting data in the games module
 * 
 *  Revision History:
 *      Drew Matheson, 2014.8.3: Created
 *      Drew Matheson, 2014.8.19: Added timeouts to all requests that modify data
 */

/// <reference path="~/Scripts/Library/Angular/angular-resource.js"/>

'use strict';

var gamesServices = angular.module('games.services');

// Resource for games
gamesServices.factory('gameDataService', ['$resource',
    function ($resource)
    {
        return $resource('API/Games/:gameId', null,
        {
            // Get all the games
            query: // Example call: GameService.query()
            {
                method: 'GET',
                url: 'API/Games/',
                isArray: true
            },
            // Get a single game
            get: // Example call: gameDataService.get({ gameId: $routeParams.gameId });
            {
                method: 'GET'
            },
            // Create a game
            create: // Example call: gameDataService.post(postData[, successFunction [, errorFunction] ]);
            {
                method: 'POST',
                url: 'API/Games/',
                timeout: 10000 // 10 second timeout on the request
            },
            // Update a game
            update: // Example call: gameDataService.update({ gameId: $routeParams.gameId }, game);
            {
                method: 'PUT',
                url: 'API/Games/:gameId',
                timeout: 10000 // 10 second timeout on the request
            },
            // Delete a game
            delete: // Example call: gameDataService.delete({ gameId: $routeParams.gameId });
            {
                method: 'DELETE',
                url: 'API/Games/:gameId',
                timeout: 10000 // 10 second timeout on the request
            }
        });
    }
]);