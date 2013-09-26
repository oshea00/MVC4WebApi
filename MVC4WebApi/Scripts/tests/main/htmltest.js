/// <reference path="../../jasmine.js" />

'use strict';

describe('my simple html test', function() {
    it ('can find elementid', function () {
        //debugger;
        var html = __html__['MVC4WebApi/Scripts/tests/main/html/htmltest.html'];
        document.body.innerHTML = html;
        expect(document.getElementById('elementid')).not.toBeNull();
    });
});