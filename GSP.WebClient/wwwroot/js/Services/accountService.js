(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .service('accountService', accountService);

    accountService.$inject = ['$http'];

    function accountService($http) {
        var url = "/api/Account";
                      
       this.createCustomer = function (data) {
           return $http({
               method: 'POST',
               url: url + "/CreateCustomer",
               data: data,
               headers: { 'Content-Type': 'application/json' }
           });
       };     
    }
})();

