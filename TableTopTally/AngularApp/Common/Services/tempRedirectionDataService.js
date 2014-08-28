/* tempRedirectionDataService.js
 *  Purpose: Service module for temporary storage of data when redirecting between controllers
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.07: Created
 */

(function()
{
    'use strict';

    var ttServices = angular.module('tableTopTally.services');

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
})();