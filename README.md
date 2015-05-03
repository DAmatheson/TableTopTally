#TableTopTally

Tabletop scorer made with ASP.NET MVC 5, WebAPI 2, AngularJS, and MongoDB


##Requirements

* An instance of MongoDB listening at 127.0.0.1:27017 (Default configuration)


##Running JavaScript Tests
###With Karma

* Install Node
* npm install -g karma
* npm install -g karma-cli
* npm install -g karma-chrome-launcher
* npm install -g karma-firefox-launcher
* npm install -g karma-ie-launcher
* npm install -g karma-ng-html2js-preprocessor

*Optional: have an HTML report available at localhost:9876/debug.html*
* npm install karma-jasmine-html-reporter

Open up a command prompt and move to the base directory containing
TableTopTally and TableTopTally.Tests

Then type:<br>
karma start


###With ReSharper
####(Not all tests work properly with ReSharper)

ReSharper -> Tools Section -> Unit Testing -> JavaScript Tests -> Check Enable Jasmine support
ReSharper -> Tools Section -> Unit Testing -> JavaScript Tests -> Set Jasmine version to 2.0