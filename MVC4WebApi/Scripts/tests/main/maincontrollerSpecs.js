/// <reference path="../../angular.js" />
/// <reference path="../../angular-resource.js" />
/// <reference path="../../angular-mocks.js" />
/// <reference path="../../sinon-1.7.3.js" />
/// <reference path="../../jasmine.js" />
/// <reference path="../../apps/main/main.js" />
/// <reference path="../../apps/main/maincontroller.js" />
'use strict';

describe('MainController Spec', function () {
    var scope;
    var controller;
    var q;

    beforeEach(module('mainApp'));

    beforeEach(inject(function ($controller, $rootScope, $q) {
        scope = $rootScope.$new();
        controller = $controller;
        q = $q;
    }));

    it('should set the scope title', function () {

        var mockAccountSvc;
        var mockAccounts = [];
        mockAccountSvc = sinon.stub({
            getAccounts: function () { },
            getAccountStats: function () { },
            getPagedAccounts: function () { },
        });
        var deferred1 = q.defer();
        var deferred2 = q.defer();
        deferred1.resolve(function () { return { Count: 0, TotalBalance: 0.0 } });
        deferred2.resolve(function () { return mockAccounts; });
        mockAccountSvc.getAccounts.returns(mockAccounts);
        mockAccountSvc.getAccountStats.returns(deferred1.promise);
        mockAccountSvc.getPagedAccounts.returns(deferred2.promise);

        var ctrl = controller('MainController',
            { $scope: scope, accountSvc: mockAccountSvc });

        expect(scope.title).toBeDefined();

    });

    it('should set the scope accounts to the result of accountSvc.getPagedAccounts', function () {
        var mockAccountSvc;
        var mockAccounts = [];
        mockAccountSvc = sinon.stub({
            getAccounts: function () { },
            getAccountStats: function () { },
            getPagedAccounts: function () { },
        });
        var deferred1 = q.defer();
        var deferred2 = q.defer();
        deferred1.resolve(function () { return { Count: 1, TotalBalance: 10.0 } });
        deferred2.resolve(function () { return mockAccounts; });
        mockAccountSvc.getAccounts.returns(mockAccounts);
        mockAccountSvc.getAccountStats.returns(deferred1.promise);
        mockAccountSvc.getPagedAccounts.returns(deferred2.promise);

        var ctrl = controller('MainController',
            { $scope: scope, accountSvc: mockAccountSvc });

        expect(scope.accounts).toBe(mockAccounts);

    });

});

