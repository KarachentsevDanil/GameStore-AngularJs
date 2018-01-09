(function () {
    'use strict';

    angular.module('GameStoreApp').controller('gameController', gameController);

    gameController.$inject = ['$q', '$scope', 'gameService', 'categoryService', 'AlertService'];

    function gameController($q, $scope, gameService, categoryService, alertService) {
        $scope.StartPrice = 0;
        $scope.EndPrice = 100;

        //Slider config
        $scope.SliderSettings = {
            minValue: 0,
            maxValue: 100,
            options: {
                floor: 0,
                ceil: 100,
                step: 5
            }
        };

        $scope.setSliderSettings = function() {
            var minPrice = _.min($scope.Games, function(game) { return game.Price; }).Price;
            var maxPrice = _.max($scope.Games, function(game) { return game.Price; }).Price;

            $scope.StartPrice = minPrice;
            $scope.EndPrice = maxPrice;

            $scope.SliderSettings.minValue = minPrice;
            $scope.SliderSettings.maxValue = maxPrice;
            $scope.SliderSettings.options.floor = minPrice;
            $scope.SliderSettings.options.ceil = maxPrice;
        };

        $scope.gamesViewModes = [{ name: "All games", value: 0 }, { name: "Top sell games", value: 1 }, { name: "Top rate games", value: 2 }];

        $scope.$on("slideChanged", function () {
            $scope.filterGames();
        });

        $scope.getCategories = function () {
            categoryService.getCategories()
                .success(function (category) {
                    $scope.category = category;
                }).error(function () {
                    alertService.showError("Error occure when getting categories.");
                });
        };

        $scope.selectGame = function (game) {
            $scope.EditName = game.Name;
            $scope.EditPrice = game.Price;
            $scope.EditDescription = game.Description;
            $scope.EditGameId = game.GameId;
        };

        $scope.getGameByName = function (data) {
            gameService.getGameByName(data)
                .success(function (games) {
                    $scope.Games = games;
                }).error(function myfunction() {
                    alertService.showError("Input is empty.");
                });
        };

        $scope.loadCustomerGame = function(customer) {
            $scope.customer = customer;
            var params = $scope.getFilterParams();
            $scope.getGamesByParams(params, true);
        };

        $scope.getFilterParams = function() {
            var categoryIds = new Array();
            var checkedCategories = $('.category-value:checked');

            for (var i = 0; i < checkedCategories.length; i++) {
                categoryIds.push($(checkedCategories[i]).data('category-id'));
            }

            var params = {
                CategoriesIds: categoryIds,
                Term: $scope.searchValue,
                StartPrice: $scope.StartPrice,
                EndPrice: $scope.EndPrice,
                Customer: $scope.customer,
                OutputMode: $scope.outputMode
            };

            return params;
        };

        $scope.filterGames = function () {
            var params = $scope.getFilterParams();
            $scope.getGamesByParams(params);
        };

        $scope.getGamesByParams = function(params, setSliderSettings) {
            gameService.getGameByParams(params)
                .success(function(games) {
                    $scope.Games = games;

                    if (setSliderSettings) {
                        $scope.setSliderSettings();
                    }
                }).error(function() {
                    alertService.showError("Error occure when getting games.");
                });
        };

        $scope.getGame = function () {
            gameService.getGame()
                .success(function (games) {
                    $scope.Games = games;
                    $scope.setSliderSettings();
                }).error(function () {
                    alertService.showError("Error occure when getting games.");
                });
        };

        var editGamePromise = function (gameRequest) {
            return $q(function (resolve, reject) {
                gameService
                    .editGame(gameRequest)
                    .success(function (gameAccount) {
                        resolve(gameAccount);
                    }).error(function () {
                        alertService.showError("Error occure when getting games.");
                        reject();
                    });
            });
        };

        var deleteGamePromise = function (gameRequest) {
            return $q(function (resolve, reject) {
                gameService
                    .deleteGame(gameRequest)
                    .success(function (gameId) {
                        resolve(gameId);
                    }).error(function () {
                        alertService.showError("Error occure when deleting games.");
                        reject();
                    });
            });
        };

        $scope.deleteGame = function (gameId) {
            $scope.deleteGamePromise = deleteGamePromise(gameId);
            $scope.deleteGamePromise.then(function () {
                alertService.showSuccess("Game was successfully deleted.");
                $scope.getGame();
            });
        };

        $scope.editGame = function () {
            var gameRequest = {
                Name: $scope.EditName,
                GameId: $scope.EditGameId,
                Price: $scope.EditPrice,
                Description: $scope.EditDescription
            };

            if (!gameRequest.GameId) {
                alertService.showError("Please select game.");
                return;
            }

            $scope.editGamePromise = editGamePromise(gameRequest);
            $scope.editGamePromise.then(function () {
                alertService.showSuccess("Game was successfully edited.");
                $scope.getGame();
            });
        };

    }
})();