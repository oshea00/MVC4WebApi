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

    beforeEach(module('mainApp'));

    beforeEach(inject(function ($controller, $rootScope) {
        scope = $rootScope.$new();
        controller = $controller;
    }));

    it('should set the scope title', function () {

        var mockAccountSvc;
        var mockAccounts = [];

        mockAccountSvc = sinon.stub({ getAccounts: function () { } });
        mockAccountSvc.getAccounts.returns(mockAccounts);

        var ctrl = controller('MainController',
            { $scope: scope, accountSvc: mockAccountSvc });

        expect(scope.title).toBeDefined();

    });

    it('should set the scope accounts to the result of accountSvc.getAccounts', function () {
        var mockAccountSvc;
        var mockAccounts = [];

        mockAccountSvc = sinon.stub({ getAccounts: function () { } });
        mockAccountSvc.getAccounts.returns(mockAccounts);

        var ctrl = controller('MainController',
            { $scope: scope, accountSvc: mockAccountSvc });

        expect(scope.accounts).toBe(mockAccounts);

    });

});

