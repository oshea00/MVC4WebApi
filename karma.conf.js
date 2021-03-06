// Karma configuration
// Generated on Wed Sep 04 2013 19:55:27 GMT-0700 (Pacific Daylight Time)

module.exports = function(config) {
  config.set({

    // base path, that will be used to resolve files and exclude
    basePath: './',


    // frameworks to use
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
        'MVC4WebApi/Scripts/jquery-1.8.2.js',
        'MVC4WebApi/Scripts/bootstrap.js',
        'MVC4WebApi/Scripts/jquery.signalR-1.0.0.js',
        'MVC4WebApi/Scripts/angular.js',
        'MVC4WebApi/Scripts/angular-resource.js',
        'MVC4WebApi/Scripts/angular-mocks.js',
        'MVC4WebApi/Scripts/underscore.js',
        'MVC4WebApi/Scripts/sinon-1.7.3.js',
        'MVC4WebApi/Scripts/ui-bootstrap-0.6.0.js',
        //'MVC4WebApi/Scripts/ui-bootstrap-0.5.0.js',
        'MVC4WebApi/Scripts/apps/main/main.js',
        'MVC4WebApi/Scripts/apps/main/notifySvc.js',
        'MVC4WebApi/Scripts/apps/main/accountSvc.js',
        'MVC4WebApi/Scripts/tests/main/html/*.html',
        'MVC4WebApi/Scripts/apps/**/*.js',
        'MVC4WebApi/Scripts/tests/**/*.js',
    ],


    // list of files to exclude
    exclude: [
    ],


    // test results reporter to use
    // possible values: 'dots', 'progress', 'junit', 'growl', 'coverage'
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


    // Start these browsers, currently available:
    // - Chrome
    // - ChromeCanary
    // - Firefox
    // - Opera
    // - Safari (only Mac)
    // - PhantomJS
    // - IE (only Windows)
    browsers: ['Chrome'],


    // If browser does not capture in given timeout [ms], kill it
    captureTimeout: 60000,


    // Continuous Integration mode
    // if true, it capture browsers, run tests and exit
    singleRun: false
  });
};
