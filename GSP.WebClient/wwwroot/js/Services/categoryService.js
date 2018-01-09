(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .service('categoryService', categoryService);

    categoryService.$inject = ['$http'];

    function categoryService($http) {
        var baseCategoryUrl = "/api/category";
        var getCategoriesUrl = baseCategoryUrl.concat("/GetCategories");

        this.getCategories = function () {
            return $http.get(getCategoriesUrl);
        };              
    }
})();