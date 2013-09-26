'use strict';

mainApp.controller('MainController',
    function MainController($scope, $modal, accountSvc) {
        $scope.accounts = accountSvc.getAccounts();
        $scope.title = 'Accounts';

        $scope.updateItem = function (item) {
            var accountModal = $modal.open({
                templateUrl: 'accountContent',
                controller: 'accountItemController',
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

            accountModal.result.then(
                function (account) {
                    angular.copy(account,item.account);
                },
                function (cancelvalue) {
                    var val = cancelvalue;
                }
            );

        }

    });

