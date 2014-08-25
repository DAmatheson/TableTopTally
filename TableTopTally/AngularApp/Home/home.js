/* home.js
 *  Purpose: Angular sub-module for the home page in TableTop Tally
 *
 *  Revision History:
 *      Drew Matheson, 2014.08.20: Created
*/

/// <reference path="~/Scripts/Library/Angular/angular.js"/>

'use strict';

// Declare home sub-module which depends on its own routes
var ttHome = angular.module('home', [
    'home.routes'
]);