/* GameDataService.js
 *  Purpose: Service module for getting data in the games module
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.03: Created
 *      Drew Matheson, 2014.08.19: Added timeouts to all requests that modify data
 */

(function()
{
    'use strict';

    var gamesServices = angular.module('games.services');

    // Resource for games
    gamesServices.factory('gameData', [
        '$resource',
        function($resource)
        {
            return $resource('API/Games/:gameId', null,
            {
                // Get all the games
                query: // Example call: gameData.query()
                {
                    method: 'GET',
                    url: 'API/Games/',
                    isArray: true
                },
                // Get a single game
                get: // Example call: gameData.get({ gameId: $routeParams.gameId });
                {
                    method: 'GET'
                },
                // Create a game
                create: // Example call: gameData.post(postData[, successFunction [, errorFunction] ]);
                {
                    method: 'POST',
                    url: 'API/Games/',
                    timeout: 10000 // 10 second timeout on the request
                },
                // Update a game
                update: // Example call: gameData.update({ gameId: $routeParams.gameId }, game);
                {
                    method: 'PUT',
                    url: 'API/Games/:gameId',
                    timeout: 10000 // 10 second timeout on the request
                },
                // Delete a game
                delete: // Example call: gameData.delete({ gameId: $routeParams.gameId });
                {
                    method: 'DELETE',
                    url: 'API/Games/:gameId',
                    timeout: 10000 // 10 second timeout on the request
                }
            });
        }
    ]);
})();