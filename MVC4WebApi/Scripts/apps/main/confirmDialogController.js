'use strict';

mainApp.controller('confirmDialogController',
    function confirmDialogController($scope, $modalInstance, account) {

        $scope.account = angular.copy(account);

        $scope.dismiss = function () {
            $modalInstance.dismiss('cancel');
        }

        $scope.deleteAccount = function () {
            $modalInstance.close($scope.account);
        }

    });
