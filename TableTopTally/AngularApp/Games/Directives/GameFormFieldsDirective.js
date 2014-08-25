/* GameFormFieldsDirective.js
 * Purpose: Directive for displaying the game form fields in the <game-form-fields> html element
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.18: Created
 */

'use strict';

var gamesDirectives = angular.module('games.directives');

gamesDirectives.directive('gameFormFields',
    function ()
    {
        return {
            restrict: 'E', // Restrict to element
            templateUrl: 'AngularApp/Games/Partials/GameFormFields.html'
        };
    }
);