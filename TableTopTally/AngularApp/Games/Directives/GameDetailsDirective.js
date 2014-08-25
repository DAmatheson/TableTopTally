/* GameDetailsDirective.js
 * Purpose: Directive to display game details in the <game-details> html element
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.18: Created
 */

'use strict';

var gamesDirectives = angular.module('games.directives');

gamesDirectives.directive('gameDetails',
    function ()
    {
        return {
            restrict: 'E', // Restrict to element
            templateUrl: 'AngularApp/Games/Partials/GameDetails.html'
        };
    }
);