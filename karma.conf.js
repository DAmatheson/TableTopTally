// Karma configuration
// Generated on Sat Jan 10 2015 22:32:14 GMT-0500 (Eastern Standard Time)

module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
      'TableTopTally/Scripts/Library/modernizr-2.7.2.js',
      'TableTopTally/Scripts/Library/jQuery/jquery-2.1.1.js',
      'TableTopTally/Scripts/Library/Angular/angular.js',
      'TableTopTally/Scripts/Library/Angular/angular-resource.js',
      'TableTopTally/Scripts/Library/Angular/angular-route.js',
      'TableTopTally/Scripts/Library/Angular/angular-mocks.js',
      'TableTopTally/Scripts/Library/Angular/angular-messages.js',
      'TableTopTally/Scripts/Library/AngularLoadingBar/loading-bar.js',
      
      'TableTopTally/AngularApp/*.js',
      'TableTopTally/AngularApp/*/*.js',
      'TableTopTally/AngularApp/**/*.js',
      
      'TableTopTally.AngularApp.Tests/*.spec.js',
      'TableTopTally.AngularApp.Tests/**/*.spec.js'
    ],


    // list of files to exclude
    exclude: [
      'TableTopTally/Scripts/Library/Angular/*.min.js',
      'TableTopTally/Scripts/Library/Angular/angular-scenario.js'
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: [
      'Chrome',
      'Firefox',
      'IE'
    ],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false
  });
};
