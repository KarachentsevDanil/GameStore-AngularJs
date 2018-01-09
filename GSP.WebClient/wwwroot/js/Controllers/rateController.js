(function () {
    'use strict';

    angular.module('GameStoreApp').controller('rateController', rateController);

    rateController.$inject = ['$q', '$scope', 'rateService', 'orderService', 'AlertService'];

    function rateController($q, $scope, rateService, orderService, alertService) {

        var createCreateFeedPromise = function (feedRequest) {
            return $q(function (resolve, reject) {
                rateService.сreateFeed(feedRequest)
                    .success(function (customerFeed) {
                        resolve(customerFeed);
                    }).error(function () {
                        console.error("Error occure when adding comment to game.");
                        reject();
                    });
            });
        };

        $scope.refreshComment = function (gameId) {
            $scope.feed = null;
            $scope.getFeedsByGameId(gameId);
        }

        $scope.createFeed = function (gameId, customer) {
            var rateRequest = {
                Customer: customer,
                GameId: gameId,
                Comment: $scope.feed.feedContent,
                CustomerRate: $scope.feed.rating
            };

            $scope.createFeedPromise = createCreateFeedPromise(rateRequest);
            $scope.createFeedPromise.then(function () {
                $scope.refreshComment(gameId);
                alertService.showSuccess("Comment was successfully added.");
            });
        };

        $scope.getGameById = function (data) {
            rateService.getGameById(data)
                .success(function (game) {
                    $scope.CurrentGame = game;
                }).error(function () {
                    alertService.showError("Error occure when getting game.");
                });
        };

        $scope.getFeedsByGameId = function (data) {
            rateService.getGameRates(data)
                .success(function (feeds) {
                    $scope.Feeds = feeds;
                }).error(function () {
                    alertService.showError("Error occure when getting comments for game.");
                });
        };


        $scope.getRecomendationToGame = function (data) {
            rateService.getRecomendationToGame(data)
                .success(function (games) {
                    $scope.RecomendGames = games;
                }).error(function () {
                    alertService.showError("Error occure when getting reccomended games for game.");
                });
        };

        var createCreateAddGamePromise = function (bucketRequest) {
            return $q(function (resolve, reject) {
                orderService.addGameToBucket(bucketRequest)
                    .success(function (data) {
                        resolve(data);
                    }).error(function () {
                        console.error("Game was successfully added to bucket.");
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
                alertService.showSuccess("Error occure when adding game to bucket.");
            });
        };
    }
})();