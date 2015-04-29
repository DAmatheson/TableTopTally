/// <reference path="~/_references.js" />

describe('Module: notFound', function ()
{
    describe('notFoundController', function ()
    {
        var $scope;
        var homeController;
        var layoutValues;

        beforeEach(module('notFound.controllers'));

        beforeEach(inject(
            function ($rootScope, $controller, _layoutValues_)
            {
                $scope = {};
                layoutValues = _layoutValues_;

                homeController = $controller('notFoundController',
                {
                    $scope: $scope,
                    layoutValues: layoutValues
                });
            }
        ));

        it('sets $scope.title to 404 Not Found', function ()
        {
            expect($scope.title).toEqual('404 Not Found');
        });

        it('sets layoutValues.title to be the same title as $scope.title', function ()
        {
            expect(layoutValues.title()).toMatch($scope.title);
        });
    });
});