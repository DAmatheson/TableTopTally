/// <reference path="~/_references.js" />

describe('Module: layout', function()
{
    describe('layoutController', function()
    {
        var layoutController;
        var layoutValues;
        var $scope;

        beforeEach(module('layout'));

        beforeEach(inject(function ($controller, _layoutValues_)
        {
            $scope = {};
            layoutValues = _layoutValues_;

            layoutController = $controller('layoutController',
            {
                $scope: $scope,
                layoutValues: layoutValues
            });
        }));

        it('sets $scope.layout to equal passed in layoutValues', function()
        {
            expect($scope.layout).toEqual(layoutValues);
        });
    });
});