(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .controller('categoryController', categoryController);

    categoryController.$inject = ['$scope', '$q', 'categoryService', 'AlertService'];

    function categoryController($scope, $q, categoryService, alertService) {

        $scope.getCategories = function () {
            categoryService.getCategories()
                .success(function (category) {
                    $scope.category = category;
                }).error(function () {
                    alert("Error occure when getting categories.");
                });
        };

        var addCategoryPromise = function (category) {
            return $q(function (resolve, reject) {
                categoryService
                    .addCategory(category)
                    .success(function () {
                        resolve();
                    }).error(function () {
                        alertService.showError("Error occure when adding category.");
                        reject();
                    });
            });
        };

        var closePopup = function() {
            $('#addCategory').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        $scope.addCategory = function () {
            var category = {
                Name: $scope.name
            };

            $scope.addCategoryPromise = addCategoryPromise(category);
            $scope.addCategoryPromise.then(function () {
                alertService.showSuccess("Category was successfully added.");
                closePopup();
                $scope.$emit('categoryAdded');
            });
        };
    }
})();