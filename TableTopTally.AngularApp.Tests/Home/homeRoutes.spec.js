/// <reference path="~/_references.js" />

describe('Routes: homeRoutes', function ()
{
    var $route;

    beforeEach(module('home'));

    beforeEach(inject(function (_$route_)
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