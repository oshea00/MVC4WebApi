'use strict';

mainApp.controller('MainController',
    function MainController($scope, $modal, accountSvc) {
        $scope.pageSize = 10;
        $scope.currentPage = 1;
        $scope.maxPages = 10;
        $scope.title = 'Accounts';
        $scope.pageTotal = 0.0;
        $scope.totalItems = 0;
        $scope.totalBalance = 0.0;
        $scope.orderByColumn = 'AccountCode';
        $scope.reverse = false;

        $scope.accountPagedCallback = function(accounts) {
            $scope.accounts = accounts;
            $scope.pageTotal = 0.0;
            _.each(accounts, function (a) {
                $scope.pageTotal += a.Balance;
            });
        }

        $scope.statsCallback = function (stats) {
            $scope.totalItems = stats[0].Count;
            $scope.totalBalance = stats[0].TotalBalance;
            accountSvc.getPagedAccounts($scope.currentPage - 1, $scope.pageSize, $scope.orderByColumn)
                .then($scope.accountPagedCallback);
        }

        $scope.searchCallback = function (matches) {
            $scope.accounts = matches;
            $scope.totalItems = $scope.accounts.length;
            $scope.pageTotal = 0.0;
            $scope.totalBalance = 0.0;
            _.each($scope.accounts, function (a) {
                $scope.totalBalance += a.Balance;
            });
            if ($scope.totalItems === 0) {
                $scope.searchText = "" // indicate none found more explicitly later 
                $scope.getPage();
            }
        }

        $scope.orderBy = function (colname) {
            $scope.reverse = !$scope.reverse;
            if ($scope.reverse) {
                $scope.orderByColumn = '-' + colname;
                $scope.getPage();
            }
            else {
                $scope.orderByColumn = colname;
                $scope.getPage();
            }
        }

        $scope.searchFor = function () {
            accountSvc.getBySearch($scope.searchText)
                .then(function (results) {
                    $scope.searchCallback(results);
                });
        }

        $scope.getPage = function (page) {
            if (page != null)
                $scope.currentPage = page;
            accountSvc.getAccountStats(true)
                .then($scope.statsCallback);
        }
        $scope.getPage();

        $scope.addAccountDialog = function () {
            var add = $modal.open({
                templateUrl: 'accountDialog',
                controller: 'accountDialogController',
                backdrop: false,
                resolve: {
                    account: function () {
                        return { Id: 0, AccountCode: '', Name: null, AccountName: '', Balance: 0, BalanceDate: null, IsActive: true, Version: 2.0 };
                    },
                    title: function () {
                        return 'Add Account';
                    }
                }
            });
            add.result.then(
                function (account) {
                    var promise = accountSvc.saveAccount(account);
                    promise.then(function (item) {                       
                        $scope.accounts.push(item);
                    });
                },
                function (cancelvalue) {
                    var val = cancelvalue;
                }
            );
        }

        $scope.updateAccountDialog = function (item) {
            var update = $modal.open({
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
            update.result.then(
                function (account) {
                    var promise = accountSvc.saveAccount(account);
                    promise.then(function (response) {
                        // Copy updated account values to item.account
                        angular.copy(account, item.account);
                    });
                },
                function (cancelvalue) {
                    var val = cancelvalue;
                }
            );
        }

        $scope.deleteDialog = function (item) {
            var confirm = $modal.open({
                templateUrl: 'deleteDialog',
                controller: 'deleteDialogController',
                backdrop: false,
                resolve: {
                    account: function () {
                        return item.account;
                    }
                }
            });
            confirm.result.then(
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
        }

    });

