/* notFoundController.js
 * Purpose: Controller for the 404 Not Found page
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.27: Created
 */

(function ()
{
    'use strict';

    var notFoundControllers = angular.module('notFound.controllers', []);

    // Controller for the partial DeleteGame.html
    notFoundControllers.controller('notFoundController',
    [
        '$scope', 'layoutValues',
        function ($scope, layoutValues)
        {
            $scope.title = '404 Not Found';

            layoutValues.setTitle($scope.title);
        }
    ]);
})();