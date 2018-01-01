(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .service('orderService', orderService);

    orderService.$inject = ['$http'];

    function orderService($http) {
        var url = "/api/Order";
        var allOrderurl = "/api/Order/GetAllOrder";
        var questCategoryUrl = "/api/Order/GetGamesFromBucket?customer=";
        var questCustomerUrl = "/api/Order/GetCusomerGame?customer=";
        var questOrderUrl = "/api/Order/GetCustomerOrder?customer=";
  

        this.getAllOrders = function () {
            return $http.get(allOrderurl);
        };

        this.getGamesFromBucket = function (data) {
            return $http.get(questCategoryUrl + data);
        };
        this.getCustomerGames = function (data) {
            return $http.get(questCustomerUrl + data);
        };

        this.getCustomerOrders = function (data) {
            return $http.get(questOrderUrl + data);
        };

        this.createOrder = function (data) {
            return $http({
                method: 'POST',
                url: url + "/CreateOrder",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.updateOrder = function (data) {
            return $http({
                method: 'POST',
                url: url + "/ConfirmOrder",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.addGameToBucket = function (data) {
            return $http({
                method: 'POST',
                url: url + "/AddToBucket",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.deleteGameFromBucket = function (data) {
            return $http({
                method: 'POST',
                url: url + "/DeleteGameFromBucket",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.deleteGameFromOrder = function (data) {
            return $http({
                method: 'POST',
                url: url + "/DeleteGameFromOrder",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
        this.deleteOrder = function (data) {
            return $http({
                method: 'POST',
                url: url + "/DeleteOrder",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();

