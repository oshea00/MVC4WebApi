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
