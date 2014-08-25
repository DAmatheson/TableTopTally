/* ttSubmitDirective.js
 * Purpose: Angular directive to simplify form validation
 * 
 * Revision History:
 *      Drew Matheson, 2014.08.14
 */

var ttDirectives = angular.module('tableTopTally.directives');

ttDirectives.directive('ttSubmit', ['$parse',
    function ($parse)
    {
        return {
            restrict: 'A', // Restrict this directive to attributes
            require: ['ttSubmit', '?form'], // The directive requires the controllers ttSubmit and the form's controller
            controller:
                function ()
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
                            return fieldModelController.$invalid &&
                                (fieldModelController.$dirty || this.attempted);
                        }
                        else
                        {
                            return formController &&
                                formController.$invalid &&
                                (formController.$dirty || this.attempted);
                        }
                    };
                },
            compile: function(cElement, cAttributes)
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
                        var fn = $parse(attributes.ttSubmit); // Parse tt-submit attribute Ang expression

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
                                return false; // return false to cancel form submission
                            }

                            // Only called if the form is valid
                            scope.$apply(function()
                            {
                                // Call the parsed angular expression from the tt-submit attribute
                                fn(scope, { $event: event });
                            });
                        }

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