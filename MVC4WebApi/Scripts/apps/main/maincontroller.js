'use strict';

mainApp.controller('MainController',
    function MainController($scope, $modal, accountSvc) {

        $scope.pageSize = 10;
        $scope.currentPage = 1;
        $scope.maxSize = 10;
        $scope.title = 'Accounts';

        $scope.accountPagedCallback = function(accounts) {
            $scope.accounts = accounts;
        }

        $scope.statsCallback = function (stats) {
            $scope.totalItems = stats[0].Count;
            $scope.totalBalance = stats[0].TotalBalance;
            accountSvc.getPagedAccounts($scope.currentPage - 1, $scope.pageSize)
                .then($scope.accountPagedCallback);
        }

        $scope.getPage = function (page) {
            if (page != null)
                $scope.currentPage = page;
            accountSvc.getAccountStats(true)
                .then($scope.statsCallback);
        }
        $scope.getPage();

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

