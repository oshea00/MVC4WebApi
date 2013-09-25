'use strict';

mainApp.controller('MainController',
    function MainController($scope, $modal, accountSvc) {
        $scope.accounts = accountSvc.getAccounts();
        $scope.title = 'Accounts';

        $scope.open = function (element) {

            var e = element;
            var modalInstance = $modal.open({
                templateUrl: 'accountContent.html',
                controller: 'accountItemController',
                resolve: {
                    account: function () {
                        return e.account;
                    }
                }
            });

            modalInstance.result.then(function (account) {
                e.account = account;
            },
            function (cancelvalue) {
                var val = cancelvalue;
            }
            );

        }

    });

