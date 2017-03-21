account.filter('trackingFormat',
    function () {
        return function (x) {
            if (x) {
                return "Tracked";
            } else {
                return "Not tracked";
            }
        }
    });