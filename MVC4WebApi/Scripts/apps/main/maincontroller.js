'use strict';

mainApp.controller('MainController',
    function MainController($scope, $modal, accountSvc) {
        $scope.accounts = accountSvc.getAccounts();
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

        }

    });

