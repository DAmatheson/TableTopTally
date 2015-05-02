/// <reference path="~/_references.js" />

describe('Module: lessOrEqualDirective', function()
{
    var $scope;
    var $compile;
    var form;

    beforeEach(module('tableTopTally.directives'));

    beforeEach(inject(function(_$rootScope_, _$compile_)
    {
        $scope = _$rootScope_;
        $compile = _$compile_;

        var element = angular.element(
            '<form name="test">' +
            '<input type="number" name="min" ng-model="model.min" less-or-equal="{{ model.max }}" />' +
            '<input type="number" name="max" ng-model="model.max" />' +
            '</form>'
        );

        $scope.model = { min: undefined, max: undefined };
        $compile(element)($scope);
        form = $scope.test;
    }));

    it('is valid when max is greater than min', function()
    {
        form.min.$setViewValue(0);
        form.max.$setViewValue(2);
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeFalsy();
    });

    it('is valid when min and max are equal', function()
    {
        form.min.$setViewValue(1);
        form.max.$setViewValue(1);
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeFalsy();
    });

    it('is valid when max is not a number', function ()
    {
        form.min.$setViewValue(2);
        form.max.$setViewValue('a');
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeFalsy();
    });

    it('is valid when min and max aren\'t numbers', function ()
    {
        form.min.$setViewValue('a');
        form.max.$setViewValue('b');
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeFalsy();
    });

    it('is invalid when min is greater than max', function()
    {
        form.min.$setViewValue(2);
        form.max.$setViewValue(1);
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeTruthy();
    });

    it('is invalid when min is not a number but max is', function ()
    {
        form.min.$setViewValue('a');
        form.max.$setViewValue(1);
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeTruthy();
    });

    it('revalidates on attribute value (max) change', function ()
    {
        form.min.$setViewValue(2);
        form.max.$setViewValue(1);
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeTruthy();

        form.max.$setViewValue(2);
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeFalsy();
    });

    it('revalidates on element value (min) change', function ()
    {
        form.min.$setViewValue(2);
        form.max.$setViewValue(1);
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeTruthy();

        form.min.$setViewValue(1);
        $scope.$digest();

        expect(form.min.$error.lessOrEqual).toBeFalsy();
    });
});