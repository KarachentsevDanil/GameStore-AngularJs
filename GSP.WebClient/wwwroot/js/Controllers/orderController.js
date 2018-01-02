
(function () {
    'use strict';

    angular
     .module('GameStoreApp')
     .controller('orderController', orderController);

    orderController.$inject = ['$q', '$scope', 'orderService', 'AlertService'];

    function orderController($q, $scope, orderService, alertService) {

        $scope.getGamesFromBucket = function (data) {
            orderService
                .getGamesFromBucket(data)
                .success(function(games) {
                    console.log(games);
                    $scope.GamesFromBucket = games;
                    $scope.CommonItem = $scope.GamesFromBucket.length;
                    var price = 0;
                    for (var i = 0; i < $scope.GamesFromBucket.length; i++) {
                        price += $scope.GamesFromBucket[i].Price;
                    }
                    $scope.CommonPrice = price;
                }).error(function myfunction() {
                    alert("Error:");
                });
        };

        $scope.getCustomerGames = function (data) {
            orderService
                .getCustomerGames(data)
                .success(function(games) {
                    console.log(games);
                    $scope.CustomerGames = games;
                }).error(function myfunction() {
                    alert("Error:");
                });
        };

        $scope.getAllOrders = function () {
            orderService
                .getAllOrders()
                .success(function(games) {
                    console.log(games);
                    $scope.Orders = games;
                }).error(function myfunction() {
                    alert("Error:");
                });
        };

        $scope.getCustomerOrders = function (data) {
            orderService
                .getCustomerOrders(data)
                .success(function(games) {
                    console.log(games);
                    $scope.Orders = games;
                }).error(function myfunction() {
                    alert("Error:");
                });
        };

        $scope.selectOrder = function (game,orderId) {
            $scope.GameFromOrder = game;
            $scope.CurrentOreder = orderId;
        }

        $scope.confirmOrder = function (customer) {
            var customerRequest = {
                Customer: customer
            };

            $scope.confirmOrderPromise = createConfirmOrderPromise(customerRequest);
            $scope.confirmOrderPromise.then(function () {
                alertService.showSuccess("Order is confirm");
               
            });
        };

        var createConfirmOrderPromise = function (customerRequest) {
            return $q(function (resolve, reject) {
                orderService
                    .updateOrder(customerRequest)
                           .success(function (customerAccount) {
                               resolve(customerAccount);
                           }).error(function (response) {
                               console.error("Customer account creating wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };

        $scope.createOrder = function (customer) {
            var customerRequest = {
                Customer: customer
            };
            $scope.createOrderPromise = createCreateOrderPromise(customerRequest);
            $scope.createOrderPromise.then(function () { });
        };

        var createCreateOrderPromise = function (customerRequest) {
            return $q(function (resolve, reject) {
                orderService
                    .createOrder(customerRequest)
                           .success(function (customerAccount) {
                               resolve(customerAccount);
                           }).error(function (response) {
                               console.error("Customer account creating wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };

        $scope.deleteOrder = function (deleteOrderId) {
            var orderId = deleteOrderId
            $scope.deleteOrderPromise = createDeleteOrderPromise(orderId);
            $scope.deleteOrderPromise.then(function () {
                alertService.showSuccess("Order was deleted");
                $scope.getAllOrders();
            });
        };

        var createDeleteOrderPromise = function (orderId) {
            return $q(function (resolve, reject) {
                orderService
                    .deleteOrder(orderId)
                           .success(function (id) {
                               resolve(id);
                           }).error(function (response) {
                               console.error("Customer account creating wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };

        $scope.addToBucket = function (customer, gameId) {
            var bucketRequest = {
                Customer: customer,
                GameId: gameId
            };
            $scope.createAddGamePromise = createCreateAddGamePromise(bucketRequest);
            $scope.createAddGamePromise.then(function () {
                alertService.showSuccess("Account Succsesfull added");
            });
        };

        var createCreateAddGamePromise = function (bucketRequest) {
            return $q(function (resolve, reject) {
                orderService
                    .addGameToBucket(bucketRequest)
                           .success(function (data) {
                               resolve(data);
                           }).error(function (response) {
                               console.error("Game succsesfull added to bucket!");
                               console.error(response);
                               reject();
                           });
            });
        };

        $scope.deleteGameFromBucket = function (customer, gameId) {
            var bucketRequest = {
                Customer: customer,
                GameId: gameId
            };
            $scope.createDeleteGameFromBucketPromise = createCreateDeleteGameFromBucketPromise(bucketRequest);
            $scope.createDeleteGameFromBucketPromise.then(function () {
                alertService.showSuccess("Game succsesfull deleted from bucket!");
                $scope.getGamesFromBucket(bucketRequest.Customer);

            });
        };



        var createCreateDeleteGameFromBucketPromise = function (bucketRequest) {
            return $q(function (resolve, reject) {
                orderService
                    .deleteGameFromBucket(bucketRequest)
                           .success(function (data) {
                               resolve(data);
                           }).error(function (response) {
                               console.error("Game don't deleted from bucket!");
                               console.error(response);
                               reject();
                           });
            });
        };

        $scope.deleteGameFromOrder = function (gameId) {
            var bucketRequest = {
                OrderId:  $scope.CurrentOreder,
                GameId: gameId
            };
            $scope.createDeleteGameFromOrderPromise = createCreateDeleteGameFromOrderPromise(bucketRequest);
            $scope.createDeleteGameFromOrderPromise.then(function () {
                alertService.showSuccess("Game succsesfull deleted from order!");
                $scope.getAllOrders();
                $scope.GameFromOrder = null;
                $scope.CurrentOreder = null;
            });
        };



        var createCreateDeleteGameFromOrderPromise = function (bucketRequest) {
            return $q(function (resolve, reject) {
                orderService
                    .deleteGameFromOrder(bucketRequest)
                           .success(function (data) {
                               resolve(data);
                           }).error(function (response) {
                               console.error("Game don't deleted from bucket!");
                               console.error(response);
                               reject();
                           });
            });
        };

    }
})();