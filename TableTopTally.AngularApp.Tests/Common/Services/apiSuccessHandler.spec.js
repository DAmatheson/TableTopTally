/// <reference path="~/_references.js" />

describe('Module: apiSuccessHandler', function()
{
    var apiSuccessHandler;
    var tempRedirectionData;
    var $location;

    beforeEach(module('tableTopTally.services'));

    beforeEach(inject(function (_apiSuccessHandler_, _$location_, _tempRedirectionData_)
    {
        $location = _$location_;
        spyOn($location, 'url');

        tempRedirectionData = _tempRedirectionData_;
        spyOn(tempRedirectionData, 'setData');

        apiSuccessHandler = _apiSuccessHandler_;
    }));

    it('calls $location.url with the passed in url', function()
    {
        apiSuccessHandler.redirect('/games/1');

        expect($location.url).toHaveBeenCalledWith('/games/1');
    });

    it('calls $location and tempRedirectionData to the passed in values', function()
    {
        apiSuccessHandler.redirect('/games/1', 'tempData');

        expect($location.url).toHaveBeenCalledWith('/games/1');
        expect(tempRedirectionData.setData).toHaveBeenCalledWith('tempData');
    });
});