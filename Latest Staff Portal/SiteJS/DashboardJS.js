$(document).ready(function () {
    $('.shadow-none').on('shown.bs.collapse', function () {
        $(this).find('.fa-arrow-down').removeClass('fa-arrow-down').addClass('fa-arrow-up');
    }).on('hidden.bs.collapse', function () {
        $(this).find('.fa-arrow-up').removeClass('fa-arrow-up').addClass('fa-arrow-down');
    });
});