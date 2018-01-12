(function () {
    'use strict';

    angular.module('GameStoreApp').controller('gameController', gameController);

    gameController.$inject = ['$q', '$scope', 'gameService', 'categoryService', 'AlertService'];

    function gameController($q, $scope, gameService, categoryService, alertService) {
        $scope.StartPrice = 0;
        $scope.EndPrice = 250;
        $scope.itemsPerPage = 12;
        $scope.maxSize = 5;
        $scope.currentPage = 1;
        $scope.totalItems = 0;
        $scope.outputMode = 0;
        $scope.outputModeText = "All games";

        //Slider config
        $scope.SliderSettings = {
            minValue: 0,
            maxValue: 250,
            options: {
                floor: 0,
                ceil: 250,
                step: 5
            }
        };

        $scope.gamesViewModes = [{ name: "All games", value: 0 }, { name: "Top sell games", value: 1 }, { name: "Top rate games", value: 2 }];

        $scope.$watch('outputMode', function () {
            switch ($scope.outputMode) {
                case 0:
                    $scope.outputModeText = "All games";
                    break;
                case 1:
                    $scope.outputModeText = "Top sell games";
                    break;
                case 2:
                    $scope.outputModeText = "Top rate games";
                    break;
                default:
                    break;
            }

            $scope.filterGames();
        });

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

        $scope.loadCustomerGame = function (customer) {
            $scope.customer = customer;
            var params = $scope.getFilterParams();
            $scope.getGamesByParams(params);
        };

        $scope.getFilterParams = function () {
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
                OutputMode: $scope.outputMode,
                PageNumber: $scope.currentPage,
                PageSize: $scope.itemsPerPage
            };

            return params;
        };

        $scope.filterGames = function () {
            var params = $scope.getFilterParams();
            $scope.getGamesByParams(params);
        };

        $scope.getGamesByParams = function (params) {
            gameService.getGameByParams(params)
                .success(function (result) {
                    $scope.Games = result.Collection;
                    $scope.totalItems = result.TotalCount;
                }).error(function () {
                    alertService.showError("Error occure when getting games.");
                });
        };

        $scope.getGames = function () {
            var params = $scope.getFilterParams();
            $scope.getGamesByParams(params);
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