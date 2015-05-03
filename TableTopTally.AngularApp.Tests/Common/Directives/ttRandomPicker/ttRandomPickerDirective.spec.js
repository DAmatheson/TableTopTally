/// <reference path="~/_references.js" />

describe('Module: ttRandomPickerDirective', function()
{
    var $scope;
    var randomPicker;

    beforeEach(module('htmlTemplates', 'tableTopTally.directives'));

    beforeEach(inject(function ($rootScope, $compile)
    {
        $scope = $rootScope;

        var element = angular.element('<tt-random-picker></tt-random-picker>');
        $compile(element)($scope);
        $scope.$digest();

        randomPicker = $scope.tt.randomPicker;
    }));

    it('adds .tt to $scope if it is not defined already', function ()
    {
        expect($scope.tt).toBeDefined();
    });

    it('extends $scope.tt if it already exists', function ()
    {
        inject(function($rootScope, $compile)
        {
            $scope = $rootScope;
            $scope.tt = { preDefinedValue: true }

            var element = angular.element('<tt-random-picker></tt-random-picker>');

            $compile(element)($scope);

            $scope.$digest();

            randomPicker = $scope.tt.randomPicker;
        });

        expect($scope.tt.preDefinedValue).toBe(true);
    });

    it('adds .randomPicker to $scope.tt', function ()
    {
        expect($scope.tt.randomPicker).toBeDefined();
    });

    it('sets randomResult to the single value in randomValues', function()
    {
        var values = 'singleValue';

        randomPicker.randomValues = values;
        randomPicker.pickValue();

        expect(randomPicker.randomResult).toContain(values);
    });

    it('sets randomResult to one of the values in randomValues', function()
    {
        randomPicker.randomValues = 'multiple, values, present';
        randomPicker.pickValue();

        expect(randomPicker.randomResult).toMatch(/^multiple|values|present/);
    });

    it('sets randomResult to an error string if randomValues is empty', function()
    {
        randomPicker.randomValues = '';
        randomPicker.pickValue();

        expect(randomPicker.randomResult).toContain('Please enter comma or space seperated values to pick from.');
    });

    it('includes month, day, hour, minute, second, and AM or PM in the randomResult', function()
    {
        var currentDate = new Date();

        randomPicker.randomValues = 'Test';
        randomPicker.pickValue();

        expect(randomPicker.randomResult).toContain(currentDate.getMonth() + 1);
        expect(randomPicker.randomResult).toContain(currentDate.getDate());
        expect(randomPicker.randomResult).toMatch(/\ (?:[1-9]|1[0-2])(?::[0-5][0-9]){2} [AP]M$/);
    });

    it('uses the randomNames value from ValuesService as the default randomValues', function()
    {
        var randomNames;

        inject(function(_randomNames_)
        {
            randomNames = _randomNames_;
        });

        expect(randomPicker.randomValues).toEqual(randomNames);
    });

    it('does not work when used as an attribute', function()
    {
        inject(function($rootScope, $compile)
        {
            $scope = $rootScope.$new(/* isolate: */ true);

            var element = angular.element('<p tt-random-picker></p>');
            $compile(element)($scope);
            $scope.$digest();
        });

        expect($scope.tt).toBeUndefined();
    });
});