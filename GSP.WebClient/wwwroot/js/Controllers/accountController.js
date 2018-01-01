
(function () {
    'use strict';

    angular
     .module('GameStoreApp')
     .controller('accountController', accountController);

    accountController.$inject = ['$q', '$scope', 'accountService', 'AlertService'];

    function accountController($q, $scope, accountService, AlertService) {

        $scope.country = ["Russia", "Ukraine", "Polsha"];
        $scope.language = ["English", "Russia", "Polska", "Urkaine"];

        $scope.createAccount = function (account) {
            var customerRequest = {
                FullName: account.FullName,
                Email: account.Email,
                Password: account.Password,
                DateBirthsday: account.DateBirthsday,
                Country: account.Country,
                Language: account.Language
            };
            $scope.createAccountPromise = createCreateAccountPromise(customerRequest);
            $scope.createAccountPromise.then(function () {
                AlertService.showSuccess("Account Succsesfull added");
                $scope.account = null;
            });
        };

        var createCreateAccountPromise = function (customerRequest) {
            return $q(function (resolve, reject) {
                accountService
                    .createCustomer(customerRequest)
                           .success(function (customerAccount) {
                               resolve(customerAccount);
                           }).error(function (response) {
                               console.error("Customer account creating wrong!");
                               console.error(response);
                               reject();
                           });
            });
        };

    }
})();