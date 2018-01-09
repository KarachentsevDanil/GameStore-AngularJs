(function () {
    'use strict';

    angular.module('GameStoreApp').service('orderService', orderService);

    orderService.$inject = ['$http'];

    function orderService($http) {
        var baseOrderUrl = "/api/Order";
        var getAllOrderUrl = baseOrderUrl.concat("/getallorder");
        var getGamesFromBucketUrl = baseOrderUrl.concat("/getgamesfrombucket?customer=");
        var getCustomerOrderUrl = baseOrderUrl.concat("/getcustomerorder?customer=");
        var createOrderUrl = baseOrderUrl.concat("/createorder");
        var confirmOrderUrl = baseOrderUrl.concat("/confirmorder");
        var addGameToBucketUrl = baseOrderUrl.concat("/addtobucket");
        var deleteGameFromBucketUrl = baseOrderUrl.concat("/deletegamefrombucket");
        var deleteOrderUrl = baseOrderUrl.concat("/DeleteOrder");

        this.getAllOrders = function () {
            return $http.get(getAllOrderUrl);
        };

        this.getGamesFromBucket = function (data) {
            return $http.get(getGamesFromBucketUrl.concat(data));
        };
        
        this.getCustomerOrders = function (data) {
            return $http.get(getCustomerOrderUrl + data);
        };

        this.createOrder = function (data) {
            return $http({
                method: 'POST',
                url: createOrderUrl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.updateOrder = function (data) {
            return $http({
                method: 'POST',
                url: confirmOrderUrl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.addGameToBucket = function (data) {
            return $http({
                method: 'POST',
                url: addGameToBucketUrl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.deleteGameFromBucket = function (data) {
            return $http({
                method: 'POST',
                url: deleteGameFromBucketUrl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
        
        this.deleteOrder = function (data) {
            return $http({
                method: 'POST',
                url: deleteOrderUrl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();

