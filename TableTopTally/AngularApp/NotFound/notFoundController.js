/* notFoundController.js
 * Purpose: Controller for the 404 Not Found page
 */

(function ()
{
    'use strict';

    var notFoundControllers = angular.module('notFound.controllers', ['layout']);

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