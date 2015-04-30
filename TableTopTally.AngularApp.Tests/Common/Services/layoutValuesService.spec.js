/// <reference path="~/_references.js" />

describe('Module: layoutValuesService', function()
{
    var defaultTitle = 'TableTop Tally';

    var layoutValues;

    beforeEach(module('tableTopTally.services'));

    beforeEach(inject(function (_layoutValues_)
    {
        layoutValues = _layoutValues_;
    }));

    it('sets the title when setTitle is called', function()
    {
        layoutValues.setTitle('Testing');

        expect(layoutValues.title()).toMatch('Testing');
    });

    it('appends " - TableTop Tally" to the title being set', function()
    {
        layoutValues.setTitle('Hi');

        expect(layoutValues.title()).toEqual('Hi - TableTop Tally');
    });

    it('sets default title to ' + defaultTitle, function ()
    {
        expect(layoutValues.title()).toEqual(defaultTitle);
    });

    it('trims the title being set', function ()
    {
        layoutValues.setTitle(' Test ');

        expect(layoutValues.title()).toEqual('Test - TableTop Tally');
    });

    testCase([false, null, undefined, '', ' ', 0], function (input)
    {
        it('sets the title to default if ' + JSON.stringify(input) + ' is passed', function()
        {
            layoutValues.setTitle(input);

            expect(layoutValues.title()).toEqual(defaultTitle);
        });
    });
});