/* layoutValuesService.js
 *  Purpose: Service module for various layout values such as title
 */

(function () 
{
    'use strict';

    var ttServices = angular.module('tableTopTally.services');

    // Service for setting various page values
    ttServices.service('layoutValues', function ()
    {
        var title = 'TableTop Tally';

        return {
            title: function()
            {
                return title;
            },
            setTitle: function(newTitle)
            {
                if (typeof (newTitle) === 'string')
                {
                    newTitle = newTitle.trim();
                }

                // If the new title isn't an empty string or other falsy value
                if (newTitle)
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