account.filter('notificationFilter',
    function () {
        return function (x) {
            if (x) {
                return "Notifications are enabled.";
            } else {
                return "Notifications are disabled.";
            }
        }
    });