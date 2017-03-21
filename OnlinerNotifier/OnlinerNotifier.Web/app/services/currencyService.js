app.factory('currencies', function($http, $filter) {
    var currencies = [{ name: "BYN", rate: 10000 }];
    currencies.push({ name: "BYB", rate: 1 });
    $http.get('https://www.nbrb.by/API/ExRates/Rates/145?onDate=' +
            $filter('date')(Date.now(), 'EEE%2C+dd+MMM+yyyy+21%3A00%3A00') +
            '&Periodicity=0&Cur_ID=145')
        .then(function (response) {
            currencies.push({ name: "USD", rate: response.data.Cur_OfficialRate * 10000 });
        });
    return currencies;
})