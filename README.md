<h1>
TableTopTally
</h1>
Tabletop scorer made with ASP.NET MVC 5, WebAPI 2, AngularJS, and MongoDB

<h3>
Requirements
</h3>
An instance of MongoDB listening at 127.0.0.1:27017 (Default configuration)

<h3>
To run the JavaScript tests with Karma
</h3>
Install Node
npm install -g karma
npm install -g karma-cli
npm install -g karma-chrome-launcher
npm install -g karma-firefox-launcher
npm install -g karma-ie-launcher

Open up a DOS prompt and move to the base directory containing
TableTopTally and TableTopTally.Tests

Then type:
karma start

<h3>
To run the JavaScript tests with ReSharper
</h3>
ReSharper -> Tools Section -> Unit Testing -> JavaScript Tests -> Check Enable Jasmine support
ReSharper -> Tools Section -> Unit Testing -> JavaScript Tests -> Set Jasmine version to 2.0