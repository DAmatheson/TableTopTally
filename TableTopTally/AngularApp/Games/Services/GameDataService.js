/* GameDataService.js
 *  Purpose: Service module for getting data in the games module
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.03: Created
 *      Drew Matheson, 2014.08.19: Added timeouts to all requests that modify data
 *      Drew Matheson, 2014.8.28: Added 404 handling to GET requests
 */

(function()
{
    'use strict';

    var gamesServices = angular.module('games.services');

    // Resource for games
    gamesServices.service('gameData', [
        '$resource', '$location', '$q',
        function($resource, $location, $q)
        {
            var baseUrl = 'API/Games/';

            return $resource(baseUrl + ':gameId', null,
            {
                // Get all the games
                query: // Example call: gameData.query()
                {
                    method: 'GET',
                    url: baseUrl,
                    isArray: true
                },

                // Get a single game, redirect to 404 if a game isn't returned
                get: // Example call: gameData.get({ gameId: $routeParams.gameId });
                {
                    method: 'GET',
                    interceptor: // If the response is a 404, redirect to /404 and resolve the promise
                    {
                        responseError: function(httpResponse)
                        {
                            if (httpResponse.status === 404)
                            {
                                $location.url('/404').replace();
                            }

                            return $q.reject(httpResponse);
                        }
                    }
                },

                // Create a game
                create: // Example call: gameData.create(postData[, successFunction[, errorFunction]]);
                {
                    method: 'POST',
                    url: baseUrl,
                    timeout: 30000 // 30 second timeout on the request
                },

                // Update a game
                update: // Example call: gameData.update(game[, successFunction[, errorFunction]]);
                {
                    method: 'PUT',
                    params: { gameId: '@id' }, // set :gameId to the id property of the passed in data
                    timeout: 30000 // 30 second timeout on the request
                },

                // Delete a game
                delete: // Example call: gameData.delete([successFunction[, errorFunction]]);
                {
                    method: 'DELETE',
                    timeout: 30000 // 30 second timeout on the request
                }
            });
        }
    ]);
})();