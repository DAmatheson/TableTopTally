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

ttDirectives.directive('appVersion', ['version',
    function (version)
    {
        return function (scope, elm, attrs)
        {
            elm.text(version);
        };
    }
]);