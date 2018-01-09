(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .controller('categoryController', categoryController);

    categoryController.$inject = ['$scope', 'categoryService'];

    function categoryController($scope, categoryService) {

        $scope.getCategories = function () {
            categoryService.getCategories()
                .success(function (category) {
                    $scope.category = category;
                }).error(function () {
                    alert("Error occure when getting categories.");
                });
        };
    }
})();