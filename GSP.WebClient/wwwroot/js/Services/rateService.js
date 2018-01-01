(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .service('rateService', rateService);

    rateService.$inject = ['$http'];

    function rateService($http) {
        var url = "/api/rate";
        var urlRequest ="/api/rate/GetGameRates?gameId="
        var questIdUrl = "/api/Game/GetGameById?gameId=";
        var recomendationUrl = "/api/rate/GetAllTranscations?gameId=";
        var questMostSale = "/api/Rate/GetTopSaleGame";
        var questMostSaleByCategory = "/api/Rate/GetTopSaleGameByCategory?category=";

        this.getMostRateGames = function () {
            return $http.get(url + "/GetMostRateGame");
        };

        this.getMostSaleGames = function () {
            return $http.get(questMostSale);
        };

        this.getRecomendationToGame = function (data) {
            return $http.get(recomendationUrl + data);
        };

        this.getMostSaleGamesByCategory = function (data) {
            return $http.get(questMostSaleByCategory + data);
        };

        this.getGameRates = function (data) {
            return $http.get(urlRequest + data);
        };

        this.getGameById = function (data) {
            return $http.get(questIdUrl + data);
        };

        this.сreateFeed = function (data) {
            return $http({
                method: 'POST',
                url: url + "/CreateFeed",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();