(function () {
    'use strict';

    angular.module('GameStoreApp').service('gameService', gameService);

    gameService.$inject = ['$http'];

    function gameService($http) {
        var baseGameUrl = "/api/Game";
        var getGameByParams = baseGameUrl.concat("/getgamesbyparams");
        var getRecomendedGamesUrl = baseGameUrl.concat("/getrecomendedgames?gameId=");
        var getGameByIdUrl = baseGameUrl.concat("/getgamebyid?gameId=");
        var createGameUrl = baseGameUrl.concat("/creategame");
        var updateGameUrl = baseGameUrl.concat("/editgame");
        var deleteGameUrl = baseGameUrl.concat("/deletegame");

        this.getRecomendedGames = function (gameId) {
            return $http.get(getRecomendedGamesUrl.concat(gameId));
        };
        
        this.getGameById = function (data) {
            return $http.get(getGameByIdUrl + data);
        };
        
        this.getGameByParams = function (data) {
            return $http({
                method: 'POST',
                url: getGameByParams,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.createGame = function (data) {
            return $http({
                method: 'POST',
                url: createGameUrl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.editGame = function (data) {
            return $http({
                method: 'POST',
                url: url + updateGameUrl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };

        this.deleteGame = function (data) {
            return $http({
                method: 'POST',
                url: url + deleteGameUrl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();

