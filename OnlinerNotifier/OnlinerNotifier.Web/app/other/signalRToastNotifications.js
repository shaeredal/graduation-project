'use strict';
angular.module('signalRToastNotifications', [])
.run(function($cookies) {
        toastr.options = {
            "closeButton": true,
            "positionClass": "toast-bottom-right"
        }

        var toastHub = $.connection.toastHub;
        toastHub.client.notify = function (message) {
            toastr.info(message);
        };
        toastHub.client.setUserId = function (userId) {
            $cookies.put("signalRUserId", userId);
        }
        $.connection.hub.start().done(function () {
            toastHub.server.getUserId();
        });
    })