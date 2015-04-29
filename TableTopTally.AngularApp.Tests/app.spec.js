/// <reference path="~/_references.js" />

describe('Module: app', function()
{
    describe('Routes', function()
    {
        var $route;

        beforeEach(module('tableTopTally'));

        beforeEach(inject(function(_$route_)
        {
            $route = _$route_;
        }));

        // otherwise redirect to
        it('sets otherwise template to 404.html', function()
        {
            expect($route.routes[null].templateUrl).toEqual('AngularApp/NotFound/404.html');
        });

        it('sets otherwise controller to notFoundController', function()
        {
            expect($route.routes[null].controller).toEqual('notFoundController');
        });
    });

    describe('config', function()
    {
        var $locationProvider;
        var cfpLoadingBarProvider;

        beforeEach(function()
        {
            angular.module('locationProviderConfig', ['angular-loading-bar'])
                .config(function (_$locationProvider_, _cfpLoadingBarProvider_)
                {
                    $locationProvider = _$locationProvider_;
                    cfpLoadingBarProvider = _cfpLoadingBarProvider_;

                    spyOn($locationProvider, 'html5Mode');
                });
            module('locationProviderConfig');
            module('tableTopTally');

            inject();
        });

        it('enables html5 mode', function()
        {
            expect($locationProvider.html5Mode).toHaveBeenCalledWith(true);
        });

        it('disables cfpLoadingBar\'s bar', function()
        {
            expect(cfpLoadingBarProvider.includeBar).toBe(false);
        });
    });
});