/* apiErrorParserService.js
 *  Purpose: Service module for parsing the errors return from ASP.NET Web API
 * 
 *  Revision History:
 *      Drew Matheson, 2014.8.21: Created
 */

'use strict';

var ttServices = angular.module('tableTopTally.services');

ttServices.factory('apiErrorParser', function ()
{
    var modelErrorParser = function(httpResponse)
    {
        var errors = [];

        if (httpResponse.data && httpResponse.data.modelState)
        {
            var modelErrors = httpResponse.data.modelState;

            for (var key in modelErrors)
            {
                if (modelErrors.hasOwnProperty(key))
                {
                    for (var i = 0; i < modelErrors[key].length; i++)
                    {
                        errors.push(modelErrors[key][i]);
                    }
                }
            }
        }

        return errors;
    };

    var statusCodeParser = function(statusCode)
    {
        var result;

        switch (statusCode)
        {
            case 400:
                result = "Invalid Form Data";
                break;
            case 404:
                result = "Not Found";
                break;
            default:
                result = "Generic Error";
                break;
        }

        return result;
    }

    return {
        parseModelErrors: modelErrorParser,
        parseStatusCode: statusCodeParser
    };
});