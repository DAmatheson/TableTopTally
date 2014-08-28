/* homeController.js
 * Purpose: Controller for the home page
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.27: Created
 */

(function ()
{
    'use strict';

    var homeControllers = angular.module('home.controllers');

    // Controller for the partial DeleteGame.html
    homeControllers.controller('homeController', [
        '$scope', 'layoutValues',
        function ($scope, layoutValues)
        {
            $scope.title = 'Home';

            layoutValues.setTitle($scope.title);
        }
    ]);
})();