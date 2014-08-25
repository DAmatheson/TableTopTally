﻿/* games.js
 *  Purpose: Angular sub-module for games in TableTop Tally
 *
 *  Revision History:
 *      Drew Matheson, 2014.08.20: Created
*/

/// <reference path="~/Scripts/Library/Angular/angular.js"/>

'use strict';

// Declare games sub-module which depends on its own services, routes, directives, and controllers
var ttGames = angular.module('games', [
    'games.services',
    'games.directives',
    'games.controllers',
    'games.routes'
]);

// Declare modules that are defined in more than one file here rather than in their own files so
// that they don't override each other or require a specific loading order to prevent overriding
angular.module('games.services', ['ngResource']); // games.services requires ngResource
angular.module('games.directives', []);
angular.module('games.controllers', []);