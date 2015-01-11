/// <reference path="~/AngularAppUnitTests/_references.js" />

describe('Controller: homeController', function ()
{
    var scope;
    var homeController;
    var layoutValues;

    beforeEach(module('tableTopTally'));

    beforeEach(inject(
        function ($rootScope, $controller, _layoutValues_)
        {
            scope = $rootScope.$new();
            layoutValues = _layoutValues_;

            homeController = $controller('homeController',
            {
                $scope: scope,
                layoutValues: layoutValues
            });
        }
    ));

    it('should have scope defined', function ()
    {
        expect(scope).toBeDefined();
    });

    it('should set scope.title', function()
    {
        expect(scope.title).toEqual('Home');
    });

    it('should set layoutValues.title', function()
    {
        expect(layoutValues.title()).toMatch('Home');
    });

    it('should set layoutValues.title to contain the same title as scope.title', function()
    {
        expect(layoutValues.title()).toMatch(scope.title);
    });
});