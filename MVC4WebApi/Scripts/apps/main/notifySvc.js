/// <reference path="../../angular.js" />
/// <reference path="../../jquery-1.8.2.js" />
/// <reference path="../../jquery.signalR-1.0.0.js" />
/// <reference path="main.js" />
'use strict';

mainApp.factory('notifySvc', ['$', '$rootScope', function ($, $rootScope) {
    var proxy;
    var connection;
    return {
        connect: function () {
            connection = $.hubConnection();
            proxy = connection.createHubProxy('notifyHub');
            connection.start();
            proxy.on('accountAdded', function () {
                $rootScope.$broadcast('accountAdded');
            });
        },
        isConnecting: function () {
            return connection.state === 0;
        },
        isConnected: function () {
            return connection.state === 1;
        },
        addAccount: function () {
            proxy.invoke('addAccount', account);
        },
    };
}]);