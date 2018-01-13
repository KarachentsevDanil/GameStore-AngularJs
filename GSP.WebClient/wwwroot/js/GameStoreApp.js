(function () {
    'use strict';

    function createBusyDefaults() {
        return {
            backdrop: true,
            delay: 300,
            minDuration: 700,
            templateUrl: "/templates/angularBusyTemplate.html",
            message: "Please Wait..."
        };
    }

    angular
        .module('GameStoreApp', ['toaster', 'ngAnimate', 'rzModule', 'ui.bootstrap', 'cgBusy']).value('cgBusyDefaults', createBusyDefaults());
})();