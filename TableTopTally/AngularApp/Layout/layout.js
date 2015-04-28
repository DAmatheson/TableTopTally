/* layout.js
 *  Purpose: Angular sub-module for changing values in the layout
 *
 *  Revision History:
 *      Drew Matheson, 2014.08.20: Created
*/

(function()
{
    'use strict';

    // Declare layout sub-module which depends on its own controller
    angular.module('layout', [
        'layout.controller'
    ]);

    // Depends upon tableTopTally.services for layoutValues
    angular.module('layout.controller', ['tableTopTally.services']);
})();