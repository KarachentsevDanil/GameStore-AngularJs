(function () {
    'use strict';

    var module = angular.module('GameStoreApp');

    module.directive('initSelect', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs, ngModel) {
                element.select2();
            }
        }
    });

})();