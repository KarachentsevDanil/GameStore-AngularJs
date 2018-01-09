(function () {
    'use strict';

    var module = angular.module('GameStoreApp');

    module.directive('initDatepicker', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs, ngModel) {

                var datepickerConfig = {
                    autoclose: true,
                    startDate: "1/1/1900",
                    endDate: "1/1/2030",
                    todayBtn: "linked",
                    todayHighlight: true
                };

                element.datepicker(datepickerConfig);
                element.attr('placeholder', 'mm/dd/yyyy');
            }
        }
    });

})();