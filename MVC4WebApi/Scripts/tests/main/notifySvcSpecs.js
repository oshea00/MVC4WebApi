/// <reference path="../angular.js" />
/// <reference path="../ui-bootstrap-0.5.0.js" />
/// <reference path="../angular-mocks.js" />
/// <reference path="../sinon-1.7.3.js" />
/// <reference path="../apps/main/main.js" />
/// <reference path="../apps/main/notifySvc.js" />
/// <reference path="../jasmine.js" />
'use strict';

describe('notifySvc Tests', function () {
    var notify;

    beforeEach(module('mainApp'));
    beforeEach(inject(function (notifySvc) {
        notify = notifySvc;
        notify.connect();
    }));

    it('should attempt connection', function () {
        expect(notify.isConnecting()).toBe(true);
    });

})
