/// <reference path="~/_references.js" />

describe('Module: ttSubmitDirective', function()
{
    var $scope;
    var $compile;
    var form;
    var formName = 'test';
    var element;

    function triggerSubmit()
    {
        element.triggerHandler('submit');
    }

    beforeEach(module('tableTopTally.directives'));

    beforeEach(inject(function (_$rootScope_, _$compile_)
    {
        $scope = _$rootScope_;
        $compile = _$compile_;

        element = angular.element(
            '<form name="' + formName + '" tt-submit>' +
                '<input type="number" name="min" min="10" required ng-model="model.min" />' +
            '</form>'
        );

        $scope.model = { min: undefined };
        $compile(element)($scope);
        form = $scope[formName];
    }));

    it('is called when submit happens on the form and sets attempted to true', function()
    {
        expect($scope.tt[formName].attempted).toBe(false);

        triggerSubmit();

        expect($scope.tt[formName].attempted).toBe(true);
    });

    it('adds .tt to $scope', function()
    {
        expect($scope.tt).toBeDefined();
    });

    it('adds submitController to $scope.tt.{FormName}', function()
    {
        expect($scope.tt[formName]).toBeDefined();
    });

    it('adds hasError to $scope.tt.{FormName}', function()
    {
        expect($scope.tt[formName].hasError).toBeDefined();
    });

    it('throws an error when formController is not setup', function ()
    {
        inject(function (_$rootScope_, _$compile_)
        {
            $scope = _$rootScope_;
            $compile = _$compile_;

            element = angular.element(
                '<div name="' + formName + '" tt-submit>' +
                    '<input type="number" name="min" ng-model="model.min" />' +
                '</div>'
            );
        });

        expect(function () { $compile(element)($scope); }).toThrowError();
    });

    it('calls parsed attribute expression if the form is valid', function ()
    {
        inject(function(_$rootScope_, _$compile_)
        {
            $scope = _$rootScope_;
            $compile = _$compile_;

            $scope.attrFunction = function() { };
            spyOn($scope, 'attrFunction');

            // Valid by default
            element = angular.element(
                '<form name="' + formName + '" tt-submit="attrFunction();">' +
                    '<input type="number" name="min" ng-model="model.min" />' +
                '</form>'
            );

            $scope.model = { min: undefined };
            $compile(element)($scope);
            form = $scope[formName];
        });

        expect($scope.attrFunction).not.toHaveBeenCalled();
        expect(form.min.$invalid).toBe(false);

        triggerSubmit();

        expect($scope.attrFunction).toHaveBeenCalled();
    });

    it('does not call parsed attribute expression if the form is invalid', function ()
    {
        inject(function (_$rootScope_, _$compile_)
        {
            $scope = _$rootScope_;
            $compile = _$compile_;

            $scope.attrFunction = function () { };
            spyOn($scope, 'attrFunction');

            // One required field
            element = angular.element(
                '<form name="' + formName + '" tt-submit="attrFunction();">' +
                    '<input type="number" name="min" required ng-model="model.min" />' +
                '</form>'
            );

            $scope.model = { min: undefined };
            $compile(element)($scope);
            form = $scope[formName];
        });

        $scope.$digest(); // Causes validation and updates field values if $setViewValue has been called

        expect($scope.attrFunction).not.toHaveBeenCalled();
        expect(form.min.$invalid).toBe(true);

        triggerSubmit();

        expect($scope.attrFunction).not.toHaveBeenCalled();
    });

    describe('tt[FormName].hasError function', function()
    {
        function expect_Untouched_Invalid_NotSubmitted()
        {
            expect(form.min.$dirty).toBe(false);
            expect(form.min.$invalid).toBe(true);
            expect($scope.tt[formName].attempted).toBe(false);
        }

        // Touched and errors present
        it('returns true when the field is dirty and errors are present', function ()
        {
            form.min.$setViewValue(0);
            $scope.$digest();

            expect(form.min.$dirty).toBe(true);
            expect(form.min.$invalid).toBe(true);
            expect($scope.tt[formName].attempted).toBe(false);

            expect($scope.tt[formName].hasError('min')).toBe(true);
        });

        // Touched and no errors present
        it('returns false when the field is dirty and no errors are present', function ()
        {
            form.min.$setViewValue(10);
            $scope.$digest();

            expect(form.min.$dirty).toBe(true);
            expect(form.min.$invalid).toBe(false);
            expect($scope.tt[formName].attempted).toBe(false);

            expect($scope.tt[formName].hasError('min')).toBe(false);
        });

        // Untouched but submission attempted and errors present
        it('returns true when the field is untouched but submission has been attempted and errors are present', function ()
        {
            $scope.$digest();

            triggerSubmit();

            expect(form.min.$dirty).toBe(false);
            expect(form.min.$invalid).toBe(true);
            expect($scope.tt[formName].attempted).toBe(true);

            expect($scope.tt[formName].hasError('min')).toBe(true);
        });

        // Untouched and no submission but errors are present
        it('returns false when the field is untouched and submission has not been attempted', function ()
        {
            $scope.$digest();

            expect_Untouched_Invalid_NotSubmitted();

            expect($scope.tt[formName].hasError('min')).toBe(false);
        });

        // Untouched but errors are present
        it('returns false when the field is untouched but errors are present', function ()
        {
            $scope.$digest();

            expect_Untouched_Invalid_NotSubmitted();

            expect($scope.tt[formName].hasError('min')).toBe(false);
        });

        // Untouched and no errors present
        it('returns false when the field is untouched and no errors are present', function ()
        {
            inject(function(_$rootScope_, _$compile_)
            {
                $scope = _$rootScope_;
                $compile = _$compile_;

                // Element which is valid even when untouched
                element = angular.element(
                    '<form name="' + formName + '" tt-submit>' +
                    '<input type="number" name="min" ng-model="model.min" />' +
                    '</form>'
                );

                $scope.model = { min: undefined };
                $compile(element)($scope);
                form = $scope[formName];
            });

            $scope.$digest();

            expect(form.min.$dirty).toBe(false);
            expect(form.min.$invalid).toBe(false);
            expect($scope.tt[formName].attempted).toBe(false);

            expect($scope.tt[formName].hasError('min')).toBe(false);
        });

        it('returns true when called without arg, one field is dirty+valid, and another field is untouched+invalid', function()
        {
            inject(function (_$rootScope_, _$compile_)
            {
                $scope = _$rootScope_;
                $compile = _$compile_;

                element = angular.element(
                    '<form name="' + formName + '" tt-submit>' +
                        '<input type="number" name="min" required ng-model="model.min" />' +
                        '<input type="number" name="max" required ng-model="model.max" />' +
                    '</form>'
                );

                $scope.model = { min: undefined, max: undefined };
                $compile(element)($scope);
                form = $scope[formName];
            });

            form.min.$setViewValue(1);
            $scope.$digest();

            expect(form.min.$dirty).toBe(true);
            expect(form.min.$invalid).toBe(false); // Dirty but valid

            expect(form.max.$dirty).toBe(false);
            expect(form.max.$invalid).toBe(true); // Untouched but invalid

            expect($scope.tt[formName].attempted).toBe(false); // No submission attempt

            expect($scope.tt[formName].hasError()).toBe(true);
        });

        it('works when called with a field name string', function()
        {
            $scope.$digest();

            expect_Untouched_Invalid_NotSubmitted();

            expect($scope.tt[formName].hasError('min')).toBe(false);
        });

        it('works when called with fieldModelController', function()
        {
            $scope.$digest();

            expect_Untouched_Invalid_NotSubmitted();

            expect($scope.tt[formName].hasError(form.min)).toBe(false);
        });

        it('works when called without an argument, but has a formController', function()
        {
            $scope.$digest();

            expect_Untouched_Invalid_NotSubmitted();

            expect($scope.tt[formName].hasError()).toBe(false);
        });
    });
});