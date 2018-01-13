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

        $scope.gamesViewModes = [{ name: "All Games", value: 0 }, { name: "Top Sell Games", value: 1 }, { name: "Top Rate Games", value: 2 }];

        $scope.$watch('outputMode', function () {
            var currentObject = _.find($scope.gamesViewModes, function (item) { return item.value === $scope.outputMode; });
            $scope.outputModeText = currentObject.name;
            $scope.filterGames();
        });

        $scope.$on("slideChanged", function () {
            $scope.filterGames();
        });

        $scope.$on("categoryAdded", function () {
            $scope.getCategories();
        });

        $scope.getCategories = function () {
            categoryService.getCategories()
                .success(function (category) {
                    $scope.category = category;
                }).error(function () {
                    alertService.showError("Error occure when getting categories.");
                });
        };

        $scope.clearFilterParams = function () {
            var checkedCategories = $('.category-value:checked');

            for (var i = 0; i < checkedCategories.length; i++) {
                $(checkedCategories[i]).prop("checked", false);
            }

            $scope.searchValue = "";
            $scope.StartPrice = 0;
            $scope.EndPrice = 250;
            $scope.outputMode = 0;
            $scope.currentPage = 1;
        };

        $scope.clearFilters = function () {
            $scope.clearFilterParams();
            $scope.filterGames();
        }

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
            $scope.currentPage = 1;
            var params = $scope.getFilterParams();
            $scope.getGamesByParams(params);
        };

        $scope.changePage = function () {
            var params = $scope.getFilterParams();
            $scope.getGamesByParams(params);
        };

        var getGamesByParamsPromise = function (params) {
            return $q(function (resolve, reject) {
                gameService.getGameByParams(params)
                    .success(function (games) {
                        resolve(games);
                    }).error(function () {
                        alertService.showError("Error occure when getting games.");
                        reject();
                    });
            });
        };

        $scope.getGamesByParams = function (params) {
            $scope.getGamesByParamsPromise = getGamesByParamsPromise(params);
            $scope.getGamesByParamsPromise.then(function(result) {
                $scope.Games = result.Collection;
                $scope.totalItems = result.TotalCount;
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