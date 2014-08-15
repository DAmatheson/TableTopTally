/* GameServices.js
 *  Purpose: Service module for the GameList and GameDetail controllers and partials
 * 
 *  Revision History:
 *      Drew Matheson, 2014.8.3: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular-resource.js"/>
/// <reference path="~/Scripts/Angular/app.js"/>

'use strict';

/* Services */

var ttServices = angular.module('tableTopTally.services');

ttServices.value('version', '0.0.1');

// Resource for games
ttServices.factory('gameDataService', ['$resource',
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
            get: // Example call: gameService.get({ gameId: $routeParams.gameId });
            {
                method: 'GET'
            },
            // Create a game
            post: // Example call: GameDetailService.post(postData[, callbackFunction]);
            {
                method: 'POST',
                url: 'API/Games/'
            },
            // Update a game
            update: // Example call: GameDetailService.update({ gameId: $routeParams.gameId }, game);
            {
                method: 'PUT',
            },
            // Delete a game
            delete: // Example call: GameDetailService.delete({ Id: *ObjectId* });
            {
                method: 'DELETE',
            }
        });
    }
]);

// Service for passing data from a post or put action to the corresponding display controller
ttServices.factory('tempRedirectionData', function()
{
    var tempCache = null;

    return {
        setData: function(data)
        {
            tempCache = data;
        },
        getData: function()
        {
            // Auto clear out temp data so stale data isn't used
            var holdingCache = tempCache;

            tempCache = null;

            return holdingCache;
        },
    };
});