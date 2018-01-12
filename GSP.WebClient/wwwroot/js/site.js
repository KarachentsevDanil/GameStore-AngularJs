$(document).ready(function() {
    var url = location.pathname;
    var selector = 'a[href*="' + url + '"]';
    $(selector).addClass('active');
})