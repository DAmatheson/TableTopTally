/* homeRoutes.js
 *  Purpose: Routes for the home module
 * 
 *  Revision History:
 *      Drew Matheson, 2014.8.21: Created
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>
/// <reference path="~/Scripts/Library/Angular/angular-route.js"/>
/// <reference path="~/AngularApp/Home/home.js"/>

'use strict';

var homeRoutes = angular.module('home.routes', ['ngRoute']);

homeRoutes.config(['$routeProvider',
    function ($routeProvider)
    {
        $routeProvider.when("/",
        {
            templateUrl: 'AngularApp/Home/Home.html'
        });
    }
]);