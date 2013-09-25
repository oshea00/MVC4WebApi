/// <reference path="../../angular.js" />
'use strict';

var mainApp = angular.module('mainApp', ['ngResource','ui.bootstrap'])
    .config(function ($routeProvider, $locationProvider, $httpProvider) {
        $routeProvider.when('/',
            {
                templateUrl: 'Content/apps/main/main.html',
                controller: 'MainController'
            });
        $locationProvider.html5Mode(true);
        $httpProvider.defaults.headers.common['X-Version'] = '2.0';
    });
mainApp.value('$', $);
