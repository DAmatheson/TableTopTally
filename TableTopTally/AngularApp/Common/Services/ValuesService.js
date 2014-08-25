/* ValuesService.js
 *  Purpose: Service module for angular values
 * 
 *  Revision History:
 *      Drew Matheson, 2014.08.07: Created
 *      Drew Matheson, 2014.08.25: Added randomNames value for the randomPickerDirective
 */

/// <reference path="~/Scripts/Library/Angular/angular.js"/>

'use strict';

var ttValues = angular.module('tableTopTally.services');

ttValues.value('version', '0.0.2');

ttValues.value('randomNames', 'Ian, Drew, Brittany'); // Initial value for randomPickerDirective