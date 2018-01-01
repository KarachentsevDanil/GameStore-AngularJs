(function () {
    'use strict';

    angular
     .module('GameStoreApp')
     .controller('categoryController', categoryController);

    categoryController.$inject = ['$scope', 'categoryService'];

    function categoryController($scope, categoryService) {
      
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
    }
})();