/// <reference path="../../angular.js" />
'use strict';

var mainApp = angular.module('mainApp', ['ngResource'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/',
            {
                templateUrl: 'Content/apps/main/main.html',
                controller: 'MainController'
            });
        $locationProvider.html5Mode(true);
    });
mainApp.value('$', $);
