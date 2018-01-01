(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .service('gameService', gameService);

    gameService.$inject = ['$http'];

    function gameService($http) {
        var url = "/api/Game";
        var questUrl = "/api/Game/GetGamesByName?name=";
        var questCategoryUrl = "/api/Game/GetGamesByCategory?category=";
        this.getGame = function () {
            return $http.get(url + "/GetGames");
        };

      
        this.getGameByName = function (data) {
            return $http.get(questUrl + data);
        };

        this.getGameByCategory = function (data) {
            return $http.get(questCategoryUrl + data);
        };

        this.createGame = function (data) {
            return $http({
                method: 'POST',
                url: url + "/CreateGame",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.editGame = function (data) {
            return $http({
                method: 'POST',
                url: url + "/EditGame",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.deleteGame = function (data) {
            return $http({
                method: 'POST',
                url: url + "/DeleteGame",
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };


    }
})();

