'use strict';

mainApp.controller('MainController',
    function MainController($scope, $modal, accountSvc) {
        //$scope.accounts = [];
        accountSvc.getPagedAccounts(0,25).then(function (accounts) {
            $scope.accounts = accounts;
        });

        $scope.title = 'Accounts';

        $scope.openAccountDialog = function (item) {
            var accountDialog = $modal.open({
                templateUrl: 'accountDialog',
                controller: 'accountDialogController',
                backdrop: false,
                resolve: {
                    account: function () {
                        return item.account;
                    },
                    title: function () {
                        return 'Update Account';
                    }
                }
            });

            accountDialog.result.then(
                function (account) {
                    var promise = accountSvc.saveAccount(account);
                    promise.then(function (response) {
                        angular.copy(account, item.account);
                    });
                },
                function (cancelvalue) {
                    var val = cancelvalue;
                }
            );

        };

        $scope.openConfirmDialog = function (item) {
            var confirmDialog = $modal.open({
                templateUrl: 'confirmDialog',
                controller: 'confirmDialogController',
                backdrop: false,
                resolve: {
                    account: function () {
                        return item.account;
                    }
                }
            });

            confirmDialog.result.then(
                function (account) {
                    var promise = accountSvc.deleteAccount(account);
                    promise.then(function (response) {
                        // Do something to delete from model
                        $scope.accounts.splice(item.$index, 1);
                    });
                },
                function (cancelvalue) {
                    var val = cancelvalue;
                }
            );
        };

    });

