
(function () {
    'use strict';

    angular
     .module('GameStoreApp')
     .controller('gameController', gameController);

    gameController.$inject = ['$q', '$scope', 'gameService', 'categoryService', 'AlertService'];

    function gameController($q, $scope, gameService, categoryService, AlertService) {
        $scope.firstPage = "/images/imageOne.GIF";
        $scope.imageCarousel = ["/images/imageThree.GIF", "/images/PictureTwo.GIF"]


        if (window.ClientConfig != null) {
            $scope.Name = window.ClientConfig.bagGameName;
            $scope.Price = window.ClientConfig.bagGamePrice;
            $scope.Description = window.ClientConfig.bagGameDescription;
            $scope.Image = window.ClientConfig.bagGameImage;
        }
        $scope.getCategory = function () {
            categoryService
                    .getCategory()
                    .success(function (category) {
                        console.log(category);
                        $scope.category = category;
                    }).error(function myfunction() {
                        alert("Error:");
                    })
        };

        $scope.selectGame = function (game) {
            $scope.EditName = game.Name;
            $scope.EditPrice = game.Price;
            $scope.EditDescription = game.Description;
            $scope.EditGameId = game.GameId;
        };

        $scope.getGameByName = function (data) {
            gameService
                    .getGameByName(data)
                    .success(function (games) {
                        console.log(games);
                        $scope.Games = games;
                    }).error(function myfunction() {
                        AlertService.showError("Input is empty");
                    })
        };

        $scope.getGameByCategory = function (data) {
            gameService
                    .getGameByCategory(data)
                    .success(function (games) {
                        console.log(games);
                        $scope.Games = games;
                    }).error(function myfunction() {
                        alert("Error:");
                    })
        };

        $scope.getGame = function () {
            gameService
                    .getGame()
                    .success(function (games) {
                        console.log(games);
                        $scope.Games = games;
                    }).error(function myfunction() {
                        alert("Error:");
                    })
        };



        $scope.createGame = function (categor) {
            var gameRequest = {
                Name: $scope.Name,
                Category: categor,
                Price: $scope.Price,
                Description: $scope.Description,
                Image: $scope.Image
            };
            $scope.createGamePromise = createCreateGamePromise(gameRequest);
            $scope.createGamePromise.then(function () {
                AlertService.showSuccess("Game Succsesfull added");
                $scope.Name = " ";
                $scope.Price = " ";
                $scope.Description = " ";
                $scope.Image = " ";
            });
        };



        var createCreateGamePromise = function (gameRequest) {
            return $q(function (resolve, reject) {
                gameService
                    .createGame(gameRequest)
                           .success(function (gameAccount) {
                               resolve(gameAccount);
                           }).error(function (response) {
                               console.error("Customer account creating wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };


        $scope.deleteGame = function (gameId) {
            var gameRequest = gameId;
            $scope.deleteGamePromise = createDeleteGamePromise(gameRequest);
            $scope.deleteGamePromise.then(function () {
                AlertService.showSuccess("Delete Succsesfull added");
                $scope.getGame();
            });
        };



        var createDeleteGamePromise = function (gameRequest) {
            return $q(function (resolve, reject) {
                gameService
                    .deleteGame(gameRequest)
                           .success(function (gameId) {
                               resolve(gameId);
                           }).error(function (response) {
                               console.error("Game delete wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };

        $scope.editGame = function () {
            var gameRequest = {
                Name: $scope.EditName,
                Category: $scope.EditGameId,
                Price: $scope.EditPrice,
                Description: $scope.EditDescription,
                Image: " "
            };
            $scope.editGamePromise = createEditGamePromise(gameRequest);
            $scope.editGamePromise.then(function () {
                AlertService.showSuccess("Game Succsesfull edit");
                $scope.getGame();
            });
        };



        var createEditGamePromise = function (gameRequest) {
            return $q(function (resolve, reject) {
                gameService
                    .editGame(gameRequest)
                           .success(function (gameAccount) {
                               resolve(gameAccount);
                           }).error(function (response) {
                               console.error("Customer account creating wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };

    }
})();