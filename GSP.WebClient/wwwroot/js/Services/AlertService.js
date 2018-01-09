(function () {
    'use strict';

    angular.module('GameStoreApp').service('AlertService', AlertService);

    AlertService.$inject = ['toaster'];

    function AlertService(toaster) {

        this.showWarning = function(message) {
            toaster.pop("warning", "Warning", message);
        };

        this.showSuccess = function(message) {
            toaster.pop("success", "Success", message);
        };

        this.showError = function(message) {
            toaster.pop("error", "Error", message);
        };
    }
})();