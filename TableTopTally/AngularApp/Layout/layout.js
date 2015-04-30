/* layout.js
 *  Purpose: Angular sub-module for changing values in the layout
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