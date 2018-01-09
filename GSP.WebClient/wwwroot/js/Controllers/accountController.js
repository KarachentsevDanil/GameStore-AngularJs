(function () {
    'use strict';

    angular
        .module('GameStoreApp')
        .controller('accountController', accountController);

    accountController.$inject = ['$q', '$scope'];

    function accountController($q, $scope) {
        $scope.country = ["Russia", "Ukraine", "USA"];
        $scope.language = ["English", "Russia", "Urkaine"];
    }
})();