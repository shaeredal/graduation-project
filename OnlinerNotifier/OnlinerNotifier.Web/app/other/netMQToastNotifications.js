'use strict';
angular.module('netMQToastNotifications', [])
.run(function($cookies) {
        toastr.options = {
            "closeButton": true,
            "positionClass": "toast-bottom-right"
        }

        var subscriber = new JSMQ.Subscriber();
        
        subscriber.connect("ws://localhost:81");
        subscriber.subscribe($cookies.get("User"));

        subscriber.onMessage = function (message) {
            message.popString();
            toastr.info(message.popString());
        };
    })