'use strict';
var home = angular.module('onlinerNotifier.home', ['infinite-scroll']);

home.controller('homeController', function ($scope, $http, $cookies, $location, $filter, currencies) {
       $scope.updateInfo = function() {
            var userId = $cookies.get('User');
            $http.get('api/Account/' + userId)
                .then(function (response) {
                    var user = response.data;
                    $scope.name = user.FirstName;
                    if (user.LastName != null) {
                        $scope.name += ' ' + user.LastName;
                    }
                    if (user.AvatarUri != null && user.AvatarUri.indexOf("http://") != -1) {
                        $scope.avatarUri = user.AvatarUri.replace("http://", "https://");
                    }
                    $scope.userProducts = user.UserProducts;
                    $scope.email = user.Email;

                    $scope.trackedIds = function (){
                        var idList = [];
                        for (let up of $scope.userProducts) {
                            idList.push(up.Product.OnlinerId);
                        }
                        return idList;
                    }();
                },
                function(response) {
                    if (response.status == 401) {
                        $location.path("/");
                    };
                });
        }
        $scope.updateInfo();
        

        $scope.search = function () {
            if (!$scope.searchQuery) {
                $scope.products = null;
                return;
            }
            $http.get('https://catalog.api.onliner.by/search/products?query=' + $scope.searchQuery)
                .then(function(response) {
                    $scope.products = response.data.products;
                    $scope.lastPage = response.data.page.current;
                });
        }

        $scope.more = function () {
            if (!$scope.searchQuery) {
                $scope.products = null;
                return;
            }
            $scope.lastPage += 1;
            $http.get('https://catalog.api.onliner.by/search/products?query=' +
                    $scope.searchQuery +
                    "&page=" +
                    $scope.lastPage)
                .then(function (response) {
                    $scope.products = $scope.products.concat(response.data.products);
                });
        }

        $scope.currencies = currencies;
        $scope.currentRate = $scope.currencies[0].rate;

        $scope.addProduct = function (id) {
            var productMatch = $scope.products.filter(function(prod) {
                return prod.id == id;
            });
            if (productMatch.length != 1) {
                return;
            }
            var product = productMatch[0];
            var productData = {
                "OnlinerId": product.id,
                "Name": product.full_name,
                "Image": product.images.header,
                "Url": product.html_url
            };
            if (product.prices) {
                productData["MaxPrice"] = product.prices.max;
                productData["MinPrice"] = product.prices.min;
            }
            $http.post("api/Product", productData)
                .then(function(response) {
                    $scope.updateInfo();
                }, function(response) {
                    toastr.error('"'+product.full_name+'"'+" is not added.");
                });
        }

        $scope.delProduct = function (onlinerId) {
            var id;
            for (let up of $scope.userProducts) {
                if (up.Product.OnlinerId == onlinerId) {
                    id = up.Product.Id;
                }
            }
            $http.delete('api/Product/' + id)
                .then(function (response) {
                    $scope.updateInfo();
                });
        };
    });