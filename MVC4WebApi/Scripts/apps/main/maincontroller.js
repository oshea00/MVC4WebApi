
'use strict';

mainApp.controller('MainController',
    function MainController($scope, accountSvc) {
        $scope.accounts = accountSvc.getAccounts();
        $scope.title = 'Accounts';
    });
