/// <reference path="~/_references.js" />

describe('Module: tempRedirectionDataService', function()
{
    var tempRedirectionData;

    beforeEach(module('tableTopTally.services'));

    beforeEach(inject(function (_tempRedirectionData_)
    {
        tempRedirectionData = _tempRedirectionData_;
    }));

    it('sets the passed in value', function()
    {
        tempRedirectionData.setData('Hello');

        expect(tempRedirectionData.getData()).toEqual('Hello');
    });

    it('returns null if setData hasn\'t been called', function()
    {
        expect(tempRedirectionData.getData()).toBeNull();
    });

    it('clears the stored value after getData is called', function()
    {
        tempRedirectionData.setData('Hello');

        expect(tempRedirectionData.getData()).toEqual('Hello');
        expect(tempRedirectionData.getData()).toBeNull();
    });
});