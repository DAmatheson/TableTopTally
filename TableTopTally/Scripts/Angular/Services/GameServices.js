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
ttServices.factory('GameDataService', ['$resource',
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
            get: // Example call: GameDetailService.get({ Id: *ObjectId* });
            {
                method: 'GET'
            },
            post: // Example call: GameDetailService.post(null, postData);
            {
                method: 'POST',
                url: 'API/Games/'
            },
            update: // Example call: GameDetailService.update({ Id: *ObjectId* }, game);
            {
                method: 'PUT',
                params:
                {
                    gameId: '@Id',
                }
            },
            delete: // Example call: GameDetailService.delete({ Id: *ObjectId* });
            {
                method: 'DELETE',
            }
        });
    }
]);

