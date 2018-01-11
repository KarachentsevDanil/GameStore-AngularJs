(function () {
    'use strict';

    angular.module('GameStoreApp').controller('orderController', orderController);

    orderController.$inject = ['$q', '$scope', 'orderService', 'AlertService'];

    function orderController($q, $scope, orderService, alertService) {
        $scope.itemsPerPage = 10;
        $scope.maxSize = 5;
        $scope.currentPage = 1;
        $scope.totalItems = 0;

        $scope.getFilterParams = function () {
            var params = {
                Customer: $scope.customer,
                PageNumber: $scope.currentPage,
                PageSize: $scope.itemsPerPage
            };

            return params;
        };

        $scope.filterOrders = function () {
            var params = $scope.getFilterParams();
            $scope.getOrdersByParams(params);
        };

        $scope.getOrdersByParams = function (params) {
            orderService.getOrdersByParams(params)
                .success(function (result) {
                    $scope.Orders = result.Collection;
                    $scope.totalItems = result.TotalCount;
                }).error(function () {
                    alertService.showError("Error occure when getting orders.");
                });
        };

        $scope.getAllOrders = function () {
            $scope.filterOrders();
        };

        $scope.getCustomerOrders = function (data) {
            $scope.customer = data;
            $scope.filterOrders();
        };

        $scope.getGamesFromBucket = function (data) {
            orderService.getGamesFromBucket(data)
                .success(function (games) {
                    $scope.GamesFromBucket = games;
                    $scope.CommonItem = $scope.GamesFromBucket.length;
                    $scope.CommonPrice = _.reduce(games, function (game) { return game.Price; });
                }).error(function () {
                    alert("Error occure when getting games from bucket.");
                });
        };

        $scope.selectOrder = function(game, orderId) {
            $scope.GameFromOrder = game;
            $scope.CurrentOrderId = orderId;
        };

        var confirmOrderPromise = function (customerRequest) {
            return $q(function (resolve, reject) {
                orderService.updateOrder(customerRequest)
                    .success(function (customerAccount) {
                        resolve(customerAccount);
                    }).error(function () {
                        console.error("Error occure when confirming order.");
                        reject();
                    });
            });
        };

        $scope.confirmOrder = function (customer) {
            var customerRequest = {
                Customer: customer
            };

            $scope.confirmOrderPromise = confirmOrderPromise(customerRequest);
            $scope.confirmOrderPromise.then(function () {
                alertService.showSuccess("Order was confirmed.");
                window.location = '/Game/ShowGame';
            });
        };

        var createOrderPromise = function (customerRequest) {
            return $q(function (resolve, reject) {
                orderService.createOrder(customerRequest)
                    .success(function (customerAccount) {
                        resolve(customerAccount);
                    }).error(function () {
                        console.error("Error occure when creating order for customer.");
                        reject();
                    });
            });
        };

        $scope.createOrder = function (customer) {
            var customerRequest = {
                Customer: customer
            };

            $scope.createOrderPromise = createOrderPromise(customerRequest);
            $scope.createOrderPromise.then(function () { });
        };

        var deleteOrderPromise = function (orderId) {
            return $q(function (resolve, reject) {
                orderService.deleteOrder(orderId)
                    .success(function (id) {
                        resolve(id);
                    }).error(function () {
                        console.error("Error occure when deleting order.");
                        reject();
                    });
            });
        };

        $scope.deleteOrder = function (deleteOrderId) {
            $scope.deleteOrderPromise = deleteOrderPromise(deleteOrderId);
            $scope.deleteOrderPromise.then(function () {
                alertService.showSuccess("Order was successfully deleted.");
                $scope.getAllOrders();
            });
        };

        var addGameToBucketPromise = function (bucketRequest) {
            return $q(function (resolve, reject) {
                orderService.addGameToBucket(bucketRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function () {
                        console.error("Error occure when game adding to bucket.");
                        reject();
                    });
            });
        };

        $scope.addToBucket = function (customer, gameId) {
            var bucketRequest = {
                Customer: customer,
                GameId: gameId
            };

            $scope.createAddGamePromise = addGameToBucketPromise(bucketRequest);
            $scope.createAddGamePromise.then(function () {
                alertService.showSuccess("Game was successfully added to bucket.");
            });
        };

        var deleteGameFromBucketPromise = function (bucketRequest) {
            return $q(function (resolve, reject) {
                orderService.deleteGameFromBucket(bucketRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function () {
                        console.error("Error occure when deleting game from bucket.");
                        reject();
                    });
            });
        };

        $scope.deleteGameFromBucket = function (customer, gameId) {
            var bucketRequest = {
                Customer: customer,
                GameId: gameId
            };

            $scope.createDeleteGameFromBucketPromise = deleteGameFromBucketPromise(bucketRequest);
            $scope.createDeleteGameFromBucketPromise.then(function () {
                alertService.showSuccess("Game successfully deleted from bucket.");
                $scope.getGamesFromBucket(bucketRequest.Customer);
            });
        };
    }
})();