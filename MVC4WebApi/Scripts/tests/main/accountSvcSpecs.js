/// <reference path="../../angular.js" />
/// <reference path="../../angular-resource.js" />
/// <reference path="../../angular-mocks.js" />
/// <reference path="../../sinon-1.7.3.js" />
/// <reference path="../../jasmine.js" />
/// <reference path="../../apps/main/main.js" />
/// <reference path="../../apps/main/accountSvc.js" />
'use strict';

describe('Account Service Spec', function () {

    beforeEach(module('mainApp'));

    it('should issue a GET request to /api/Account getting all accounts', inject(function (accountSvc, $httpBackend) {
        $httpBackend.expectGET('/api/Account').respond(200);
        var promise = accountSvc.getAccounts();
        $httpBackend.flush();
        expect(promise.then).toBeDefined();
    }));

});