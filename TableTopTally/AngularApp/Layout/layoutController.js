/* layoutController.js
 * Purpose: Controller for the layout
 */

(function()
{
    'use strict';

    var layoutModule = angular.module('layout.controller');

    // Controller for the page layout
    layoutModule.controller('layoutController', ['$scope', 'layoutValues',
        function($scope, layoutValues)
        {
            $scope.layout = layoutValues;
        }
    ]);
})();