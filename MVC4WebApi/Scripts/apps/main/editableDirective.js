'use strict';

mainApp.directive('editable', function () {
    return {
        restrict: 'E',
        template: '{{value}}',
        scope: {
            value: "="
        }
    }
});