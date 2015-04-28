/* home.js
 *  Purpose: Angular sub-module for the home page in TableTop Tally
*/

(function()
{
    'use strict';

    // Declare home sub-module which depends on its own routes and controllers
    angular.module('home', [
        'home.routes',
        'home.controllers'
    ]);

    // Depends upon tableTopTally.services for layoutValues
    angular.module('home.controllers', ['tableTopTally.services']);
})();