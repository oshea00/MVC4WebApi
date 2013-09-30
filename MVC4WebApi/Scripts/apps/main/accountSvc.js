/// <reference path="../../angular.js" />
/// <reference path="../../angular-resource.js" />
/// <reference path="main.js" />
'use strict';

mainApp.factory('accountSvc', function ($resource, $q) {
    var Account = $resource('/api/Account/:id', { id: '@id' });
    var AccountTotal = $resource('/api/Account/op/GetAccountTotal');
    return {
        getAccounts: function () {
            var deferred = $q.defer();
            Account.query(
                function (response) { deferred.resolve(response); },
                function (response) { deferred.reject(response); }
                );
            return deferred.promise;
        },
        saveAccount: function (account) {
            var deferred = $q.defer();
            account.$save(
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
