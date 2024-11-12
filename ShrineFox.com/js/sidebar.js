$(document).ready(function () {
    $('.menu-button').click(function () {
        $('#sidebar').addClass('show');
        $('#maincontent').addClass('show');
    });

    $('.closebtn').click(function () {
        $('#sidebar').removeClass('show');
        $('#maincontent').removeClass('show');
    });
});
