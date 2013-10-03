'use strict';

mainApp.controller('deleteDialogController',
    function deleteDialogController($scope, $modalInstance, account) {

        $scope.account = angular.copy(account);

        $scope.dismiss = function () {
            $modalInstance.dismiss('cancel');
        }

        $scope.deleteAccount = function () {
            $modalInstance.close($scope.account);
        }

    });
