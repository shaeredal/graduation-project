'use strict';
angular.module('onlinerNotifier.auth', [])
    .controller('authController', function ($scope, $http) {
        $http.get('Authorization/GetAuthUrl?providerName=Twitter')
            .then(function(response) {
                $scope.twitterLink = response.data.Url;
            });
        $http.get('Authorization/GetAuthUrl?providerName=Facebook')
            .then(function (response) {
                $scope.facebookLink = response.data.Url;
            });
        $http.get('Authorization/GetAuthUrl?providerName=Vkontakte')
            .then(function (response) {
                $scope.vkLink = response.data.Url;
            });
    });
