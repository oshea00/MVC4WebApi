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

    beforeEach(inject(function ($controller, $rootScope, $q, $log) {
        scope = $rootScope.$new();
        controller = $controller;
        q = $q;
        // mock accountSvc
        var mockAccountSvc;
        mockAccountSvc = sinon.stub({
            getAccountStats: function () { },
            getPagedAccounts: function () { },
        });
        var deferred1 = q.defer();
        mockAccountSvc.getAccountStats.returns(deferred1.promise);
        mockAccountSvc.getPagedAccounts.returns(deferred1.promise);
        // Construct controller with injected dependencies
        var ctrl = controller('MainController',
            { $scope: scope, accountSvc: mockAccountSvc });

    }));

    it('should set the scope title', function () {
        expect(scope.title).toBeDefined();

    });

    it('should set accounts collection and page total', function () {        
        var accts = [ { Balance: 10.00 } ];
        scope.accountPagedCallback(accts);
        scope.accountPagedCallback(accts);

        expect(scope.pageTotal).toBe(10.00);
//        expect(scope.pageTotal).toBe(10.00);

    });

    it('should set totalItems and totalBalance from stats', function () {
        var stats = [{ Count: 10, TotalBalance: 1000.00 }];
        scope.statsCallback(stats);

        expect(scope.totalItems).toBe(10);
        expect(scope.totalBalance).toBe(1000.00);
    });

});

