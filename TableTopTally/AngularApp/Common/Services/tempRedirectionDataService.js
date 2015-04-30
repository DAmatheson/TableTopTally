/* tempRedirectionDataService.js
 *  Purpose: Service module for temporary storage of data when redirecting between controllers
 */

(function()
{
    'use strict';

    var ttServices = angular.module('tableTopTally.services');

    // Service for passing data from a post or put action to the corresponding display controller
    ttServices.service('tempRedirectionData', function()
    {
        var tempCache = null;

        return {
            setData: function(data)
            {
                tempCache = data;
            },
            getData: function()
            {
                var holdingCache = tempCache;

                // Clear out temp data so stale data isn't used
                if (tempCache !== null)
                {
                    tempCache = null;
                }

                return holdingCache;
            },
        };
    });
})();