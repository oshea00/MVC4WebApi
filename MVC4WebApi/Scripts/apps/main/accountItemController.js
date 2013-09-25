'use strict';

mainApp.controller('accountItemController',
    function accountItemController($scope, $modalInstance, account) {

        $scope.account = account;

        $scope.dismiss = function () {
            $modalInstance.dismiss('cancel');
        }

        $scope.saveAccount = function () {
            $modalInstance.close($scope.account);
        }

    });
