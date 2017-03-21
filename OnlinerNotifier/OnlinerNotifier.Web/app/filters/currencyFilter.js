app.filter('currencyFilter', function () {
    return filterFunc;
});

var filterFunc = function(x, currentRate) {
    if (isNaN(x) || x == 0) {
        return "no offers";
    }
    return (x / currentRate).toFixed(2);
};