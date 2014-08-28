/* layoutValuesService.js
 *  Purpose: Service module for various layout values such as title
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.27: Created
 */

(function () 
{
    'use strict';

    var ttServices = angular.module('tableTopTally.services');

    // Service for setting various page values
    ttServices.factory('layoutValues', function ()
    {
        var title = 'TableTop Tally';

        return {
            title: function()
            {
                return title;
            },
            setTitle: function(newTitle)
            {
                if (newTitle) // If the new title isn't an empty string
                {
                    title = newTitle + ' - TableTop Tally';
                }
                else
                {
                    title = 'TableTop Tally';
                }
            }
        };
    });
})();