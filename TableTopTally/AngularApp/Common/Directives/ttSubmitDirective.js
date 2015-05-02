/* ttSubmitDirective.js
 * Purpose: Angular directive to simplify form validation
 */

(function()
{
    'use strict';

    var ttDirectives = angular.module('tableTopTally.directives');

    // Provides helper functions to check for errors and prevents form submission of invalid data
    ttDirectives.directive('ttSubmit', [
        '$parse',
        function($parse)
        {
            return {
                restrict: 'A', // Restrict this directive to attributes
                require: ['ttSubmit', '?form'], // The directive requires the controllers ttSubmit and the form's controller
                controller:
                    function ttSubmitController()
                    {
                        this.attempted = false;

                        var formController = null;

                        this.setAttempted = function()
                        {
                            this.attempted = true;
                        };

                        this.setFormController = function(controller)
                        {
                            if (controller === null)
                            {
                                throw Error('ttSubmit requires a form controller to work. ' +
                                    'Only use tt-submit on form elements.');
                            }

                            formController = controller;
                        };

                        this.hasError = function(fieldModelController)
                        {
                            // Allow this directive to work in ng-if by passing in the string 
                            // name of the field rather than its controller
                            if (typeof fieldModelController === 'string')
                            {
                                fieldModelController = formController[fieldModelController];
                            }

                            if (fieldModelController)
                            {
                                return fieldModelController.$invalid &&
                                (fieldModelController.$dirty || this.attempted);
                            }
                            else
                            {
                                return formController.$invalid &&
                                    (formController.$dirty || this.attempted);
                            }
                        };
                    },
                compile: function()
                {
                    return {
                        // Unsafe to do DOM element transformations in pre
                        pre: function(scope, formElement, attributes, controllers)
                        {
                            // controllers order matches the require property's array order
                            var submitController = controllers[0];
                            var formController = (controllers.length > 1) ? controllers[1] : null;

                            submitController.setFormController(formController);

                            scope.tt = scope.tt || {}; // Assign an empty object to scope.tt if it isn't set

                            // Assign the submit controller to a property named the same as the form.
                            scope.tt[attributes.name] = submitController;
                        },
                        // Safe to do DOM element transformations in post
                        post: function(scope, formElement, attributes, controllers)
                        {
                            var submitController = controllers[0];
                            var formController = (controllers.length > 1) ? controllers[1] : null;

                            // Converts the attribute value of tt-submit into a function
                            var fn = $parse(attributes.ttSubmit);

                            var formSubmitHandler = function(event)
                            {
                                // $apply updates the DOM to reflect scope changes
                                scope.$apply(function()
                                {
                                    // Set the submit attempted flag to true
                                    submitController.setAttempted();
                                });

                                if (!formController.$valid) // If the form is invalid
                                {
                                    event.preventDefault();

                                    return;
                                }

                                // Only called if the form is valid
                                scope.$apply(function()
                                {
                                    // Call the parsed angular expression from the tt-submit attribute
                                    fn(scope);
                                });
                            };

                            // On the submit event of the form, run formSubmitHandler
                            formElement.on('submit', formSubmitHandler);

                            // When the scope is destroyed, remove the jQuery formSubmitHandler
                            scope.$on('$destroy', function()
                            {
                                formElement.off('submit', formSubmitHandler);
                            });
                        }
                    };
                }
            };
        }
    ]);
})();