/* layoutController.js
 * Purpose: Controller for the layout
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.15: Created
 */

(function()
{
    'use strict';

    var layoutModule = angular.module('layout.controller');

    // Controller for the layout page
    layoutModule.controller('layoutController', ['$scope', 'layoutValues',
        function($scope, layoutValues)
        {
            $scope.layout = layoutValues;
        }
    ]);
})();