var ttDirectives = angular.module('tableTopTally.directives');

ttDirectives.directive('ttSubmit', ['$parse',
    function ($parse)
    {
        return {
            restrict: 'A', // Restrict this directive to attributes
            require: ['ttSubmit', '?form'], // The directive requires the controllers ttSubmit and the form's controller
            controller: [
                '$scope', function($scope)
                {
                    this.attempted = false;

                    var formController = null;

                    this.setAttempted = function()
                    {
                        this.attempted = true;
                    };

                    this.setFormController = function(controller)
                    {
                        formController = controller;
                    };

                    this.hasError = function(fieldModelController)
                    {
                        if (!formController)
                        {
                            return false;
                        }

                        if (fieldModelController)
                        {
                            return fieldModelController.$invalid && (fieldModelController.$dirty || this.attempted);
                        }
                        else
                        {
                            return formController && formController.$invalid && (formController.$dirty || this.attempted);
                        }
                    };
                }
            ],
            compile: function(cElement, cAttributes, transclude)
            {
                return {
                    pre: function(scope, formElement, attributes, controllers)
                    {
                        var submitController = controllers[0];
                        var formController = (controllers.length > 1) ? controllers[1] : null;

                        submitController.setFormController(formController);

                        scope.tt = scope.tt || {};
                        scope.tt[attributes.name] = submitController;
                    },
                    post: function(scope, formElement, attributes, controllers)
                    {
                        var submitController = controllers[0];
                        var formController = (controllers.length > 1) ? controllers[1] : null;
                        var fn = $parse(attributes.rcSubmit);

                        formElement.bind('submit', function(event)
                        {
                            submitController.setAttempted();

                            if (!scope.$$phase)
                            {
                                scope.$apply();
                            }

                            if (!formController.$valid)
                            {
                                return false;
                            }

                            scope.$apply(function()
                            {
                                fn(scope, { $event: event });
                            });
                        });
                    }
                };
            }
        };
    }
]);