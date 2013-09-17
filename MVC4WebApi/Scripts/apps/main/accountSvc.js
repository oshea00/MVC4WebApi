﻿/// <reference path="../../angular.js" />
/// <reference path="../../angular-resource.js" />
/// <reference path="main.js" />
'use strict';

mainApp.factory('accountSvc', function ($resource, $q) {
    var resource = $resource('/api/Account/:id', { id: '@id' });
    return {
        getAccounts: function () {
            var deferred = $q.defer();
            resource.query(
                function (response) { deferred.resolve(response); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        }
    };
});
