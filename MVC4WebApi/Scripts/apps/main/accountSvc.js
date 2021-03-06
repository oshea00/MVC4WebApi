﻿/// <reference path="../../angular.js" />
/// <reference path="../../angular-resource.js" />
/// <reference path="main.js" />
'use strict';

mainApp.factory('accountSvc', function ($resource, $q) {
    var Account = $resource('/api/Account/:id', { id: '@id' });
    return {
        getAccounts: function () {
            var deferred = $q.defer();
            Account.query(
                function (response) { deferred.resolve(response); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        },
        getAccountStats: function () {
            var deferred = $q.defer();
            Account.query(
                { stats: true },
                function (response) { deferred.resolve(response); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        },
        getPagedAccounts: function (p,s,o) {
            var deferred = $q.defer();
            Account.query({ page: p, pageSize: s, orderBy: o },
                function (response) { deferred.resolve(response); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        },
        getBySearch: function (srch) {
            var deferred = $q.defer();
            Account.query({ search: srch },
                function (response) { deferred.resolve(response); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        },
        saveAccount: function (account) {
            var deferred = $q.defer();
            Account.save(
                account,
                function (response) { deferred.resolve(response); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        },
        deleteAccount: function (account) {
        var deferred = $q.defer();
        account.$delete({ id: account.Id},
            function (response) { deferred.resolve(response); },
            function (response) { deferred.reject(response); }
            );
        return deferred.promise;
        }
    };
});
