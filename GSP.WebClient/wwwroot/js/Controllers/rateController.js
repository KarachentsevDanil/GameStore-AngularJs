
(function () {
    'use strict';

    angular
     .module('GameStoreApp')
     .controller('rateController', rateController);

    rateController.$inject = ['$q', '$scope', 'rateService', 'orderService', 'AlertService'];

    function rateController($q, $scope, rateService, orderService, AlertService) {


        $scope.createFeed = function (gameId, customer) {
            var rateRequest = {
                Customer: customer,
                GameId: gameId,
                Comment: $scope.feed.feedContent,
                CustomerRate: $scope.feed.rating

            };
            $scope.createFeedPromise = createCreateFeedPromise(rateRequest);
            $scope.createFeedPromise.then(function () {
                AlertService.showSuccess("Account Succsesfull added");
                $scope.feed = null;
                $scope.getFeedsByGameId(gameId);
            });
        };

        var createCreateFeedPromise = function (feedRequest) {
            return $q(function (resolve, reject) {
                rateService
                    .сreateFeed(feedRequest)
                           .success(function (customerFeed) {
                               resolve(customerFeed);
                           }).error(function (response) {
                               console.error("Customer account creating wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };
        $scope.getGameById = function (data) {
            rateService
                    .getGameById(data)
                    .success(function (game) {
                        console.log(game);
                        $scope.CurrentGame = game;
                    }).error(function myfunction() {
                        AlertService.showError("Something Wrong");
                    })
        };

        $scope.getFeedsByGameId = function (data) {
            rateService
                    .getGameRates(data)
                    .success(function (feeds) {
                        console.log(feeds);
                        $scope.Feeds = feeds;
                    }).error(function myfunction() {
                        AlertService.showError("Something Wrong");
                    })
        };

        $scope.getMostSaleGames = function () {
            rateService
                    .getMostSaleGames()
                    .success(function (games) {
                        console.log(games);
                        $scope.MostSaleGames = games;
                    }).error(function myfunction() {
                        AlertService.showError("Something Wrong");
                    })
        };

        $scope.getMostRateGames = function () {
            rateService
                    .getMostRateGames()
                    .success(function (games) {
                        console.log(games);
                        $scope.MostRateGames = games;
                    }).error(function myfunction() {
                        AlertService.showError("Something Wrong");
                    })
        };
        
        $scope.getMostSaleGamesByCategory = function (data) {
            rateService
                    .getMostSaleGamesByCategory(data)
                    .success(function (games) {
                        console.log(games);
                        $scope.MostSaleGames = games;
                    }).error(function myfunction() {
                        AlertService.showError("Something Wrong");
                    })
        };

        $scope.getRecomendationToGame = function (data) {
            rateService
                    .getRecomendationToGame(data)
                    .success(function (games) {
                        console.log(games);
                        $scope.RecomendGames = games;
                    }).error(function myfunction() {
                    })
        };

        $scope.addToBucket = function (customer, gameId) {
            var bucketRequest = {
                Customer: customer,
                GameId: gameId
            };
            $scope.createAddGamePromise = createCreateAddGamePromise(bucketRequest);
            $scope.createAddGamePromise.then(function () {
                AlertService.showSuccess("Account Succsesfull added");
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
    }
})();