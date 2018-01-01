(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .service('categoryService', categoryService);

    categoryService.$inject = ['$http'];

    function categoryService($http) {
        var url = "/api/category";

        this.getCategory = function () {
            return $http.get(url + "/GetCategory");
        };              
    }
})();