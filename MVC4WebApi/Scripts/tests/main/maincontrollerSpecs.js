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
    }));

    it('should set the scope title', function () {
        debugger;
        var stats = { Count: 0, TotalBalance: 0.0 };
        var mockAccountSvc;
        mockAccountSvc = sinon.stub({
            getAccounts: function () { },
            getAccountStats: function () { },
            getPagedAccounts: function () { },
        });
        var deferred1 = q.defer();
        mockAccountSvc.getAccountStats.returns(deferred1.promise);

        var ctrl = controller('MainController',
            { $scope: scope, accountSvc: mockAccountSvc });

        expect(scope.title).toBeDefined();

    });

});

