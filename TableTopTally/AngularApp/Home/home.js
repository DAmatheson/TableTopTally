/* home.js
 *  Purpose: Angular sub-module for the home page in TableTop Tally
 *
 *  Revision History:
 *      Drew Matheson, 2014.08.20: Created
*/

(function()
{
    'use strict';

    // Declare home sub-module which depends on its own routes and controllers
    angular.module('home', [
        'home.routes',
        'home.controllers'
    ]);

    angular.module('home.controllers', []);
})();