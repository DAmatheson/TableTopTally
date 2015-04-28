/// <reference path="~/_references.js" />

describe('Controller: homeController', function ()
{
    var $scope;
    var homeController;
    var layoutValues;

    beforeEach(module('home'));

    beforeEach(inject(
        function ($rootScope, $controller, _layoutValues_)
        {
            $scope = {};
            layoutValues = _layoutValues_;

            homeController = $controller('homeController',
            {
                $scope: $scope,
                layoutValues: layoutValues
            });
        }
    ));

    it('sets $scope.title', function()
    {
        expect($scope.title).toEqual('Home');
    });

    it('sets layoutValues.title to be the same title as $scope.title', function()
    {
        expect(layoutValues.title()).toMatch($scope.title);
    });
});