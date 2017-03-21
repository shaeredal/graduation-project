'use strict';
var account = angular.module('onlinerNotifier.account', []);

account.controller('accountController',
    function($scope, $http, $cookies, $location, $filter, currencies) {
        var userId = $cookies.get('User');
        $http.get('api/Account/' + userId)
            .then(function(response) {
                var user = response.data;
                $scope.name = user.FirstName;
                if (user.LastName != null) {
                    $scope.name += ' ' + user.LastName;
                }
                $scope.avatarUri = user.AvatarUri;
                $scope.userProducts = user.UserProducts;
                $scope.email = user.Email;
                $scope.enableNotifications = user.EnableNotifications;
                },
                function (response) {
                    if (response.status == 401) {
                        $location.path("/");
                    };
                });

        $scope.currencies = currencies;
        $scope.currentRate = $scope.currencies[0].rate;

        $scope.delProduct = function(id, index) {
            $http.delete('api/Product/' + id)
                .then(function(response) {
                    $scope.userProducts.splice(index, 1);
                });
        };

        $scope.setData = function() {
            if ($scope.NotificationForm.$valid) {
                $http.post('api/Notification/', { 'Time': $scope.time, Email: $scope.email })
                    .then(function (response) {
                            $scope.enableNotifications = true;
                            toastr.success("Data is set.");
                        },
                        function(response) {
                            toastr.error("Error.");
                        });
            } else {
                toastr.error("Data is not correct.");
            }
        }

        $scope.changeTrackingStatus = function(id, index) {
            var isTracked = $scope.userProducts[index].IsTracked;
            $http.post('api/Tracking', { 'id': id, 'status': !isTracked })
                .then(function(response) {
                    if (response.status == 200) {
                        $scope.userProducts[index].IsTracked = !isTracked;
                    }
                });
        }

        $scope.disableNotifications = function() {
            $http.delete('api/Notification/')
                .then(function(response) {
                    $scope.enableNotifications = false;
                    toastr.info("Notifications were disabled.");
                }, function(response) {
                    toastr.error("Can't disable notifications.");
                });
        }
    });



