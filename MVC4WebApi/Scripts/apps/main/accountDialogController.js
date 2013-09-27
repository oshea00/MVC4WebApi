'use strict';

mainApp.controller('accountDialogController',
    function accountDialogController($scope, $modalInstance, account, title) {

        $scope.account = angular.copy(account);
        $scope.title = title;

        $scope.dismiss = function () {
            $modalInstance.dismiss('cancel');
        }

        $scope.saveAccount = function () {
            $modalInstance.close($scope.account);
        }

    });
