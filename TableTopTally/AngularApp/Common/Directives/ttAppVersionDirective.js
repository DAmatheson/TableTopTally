/* ttVersionDirective.js
 *  Purpose: Angular version number attribute directive
*/

(function()
{
    'use strict';

    var ttDirectives = angular.module('tableTopTally.directives');

    ttDirectives.directive('ttAppVersion', [
        'version',
        function(version)
        {
            return {
                restrict: 'A', // Restrict it to be only an attribute
                template: 'TableTop Tally v' + version
            };
        }
    ]);
})();