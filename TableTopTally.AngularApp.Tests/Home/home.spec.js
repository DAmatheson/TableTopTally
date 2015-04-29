/// <reference path="~/_references.js" />

describe('Module: home', function()
{
    describe('homeController', function()
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

        it('sets $scope.title to Home', function ()
        {
            expect($scope.title).toEqual('Home');
        });

        it('sets layoutValues.title to be the same title as $scope.title', function ()
        {
            expect(layoutValues.title()).toMatch($scope.title);
        });
    });

    describe('Routes', function()
    {
        var $route;

        beforeEach(module('home'));

        beforeEach(inject(function(_$route_)
        {
            $route = _$route_;
        }));

        it('sets "/" controller to homeController', function()
        {
            expect($route.routes['/'].controller).toBe('homeController');
        });

        it('sets "/" templateUrl to AngularApp/Home/Home.html', function()
        {
            expect($route.routes['/'].templateUrl).toBe('AngularApp/Home/Home.html');
        });
    });
});