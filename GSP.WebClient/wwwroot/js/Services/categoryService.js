(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .service('categoryService', categoryService);

    categoryService.$inject = ['$http'];

    function categoryService($http) {
        var baseCategoryUrl = "/api/category";
        var getCategoriesUrl = baseCategoryUrl.concat("/GetCategories");
        var addCategoryUrl = baseCategoryUrl.concat("/AddCategory");

        this.getCategories = function () {
            return $http.get(getCategoriesUrl);
        };              

        this.addCategory = function (data) {
            return $http({
                method: 'POST',
                url: addCategoryUrl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            });
        };
    }
})();