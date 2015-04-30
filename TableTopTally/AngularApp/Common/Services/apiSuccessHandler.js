/* apiSuccessHandler.js
 *  Purpose: Service module for handling API Success results
 */

(function ()
{
    'use strict';

    var ttServices = angular.module('tableTopTally.services');

    // Service for redirecting to the specified url, and
    // optionally passing data returned from a POST or PUT action into tempRedirectionData
    ttServices.service('apiSuccessHandler', [
        '$location', 'tempRedirectionData',
        function ($location, tempRedirectionData)
        {
            return {
                redirect: function (url, data)
                {
                    if (data !== undefined)
                    {
                        tempRedirectionData.setData(data);
                    }

                    $location.url(url);
                }
            };
        }
    ]);
})();