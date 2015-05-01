/// <reference path="~/_references.js" />

describe('Module: ttAppVersionDirective', function()
{
    var $rootScope;
    var $compile;

    beforeEach(module('tableTopTally.directives'));

    beforeEach(inject(function (_$rootScope_, _$compile_)
    {
        $rootScope = _$rootScope_;
        $compile = _$compile_;
    }));

    it('doesn\'t work as an element', function()
    {
        var element = $compile('<tt-app-version></tt-app-version>')($rootScope);

        $rootScope.$digest();

        expect(element.html()).not.toContain('TableTop Tally v');
    });

    it('inserts version text into the element it is an attribute on', function ()
    {
        var element = $compile('<p tt-app-version></p>')($rootScope);

        $rootScope.$digest();

        expect(element[0].tagName).toBe('P');
        expect(element.html()).toContain('TableTop Tally v');
    });

    it('uses the version value from ValuesService', function()
    {
        var version;

        inject(function(_version_)
        {
            version = _version_;
        });

        var element = $compile('<p tt-app-version></tt-app-version>')($rootScope);

        $rootScope.$digest();

        expect(element.html()).toContain(version);
    });
});