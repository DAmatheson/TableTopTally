/* ttVersionDirective.js
 *  Purpose: Angular version number directive
 *
 *  Revision History:
 *      Drew Matheson, 2014.08.07: Created
*/

/// <reference path="~/Scripts/Library/Angular/angular-resource.js"/>

'use strict';

var ttDirectives = angular.module('tableTopTally.directives');

ttDirectives.directive('ttAppVersion', ['version',
    function (version)
    {
        return {
            template: "TableTop Tally v" + version
        };
    }
]);