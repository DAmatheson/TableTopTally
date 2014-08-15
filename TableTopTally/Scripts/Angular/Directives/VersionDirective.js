/* VersionDirective.js
 *  Purpose: Angular version number directive
 *
 *  Revision History:
 *      Drew Matheson, 2014.8.7: Created
*/

/// <reference path="~/Scripts/Library/Angular/angular-resource.js"/>
/// <reference path="~/Scripts/Angular/app.js"/>

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