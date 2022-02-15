jQuery(document).ready(function ($) {
    //open popup
    $('.popup-trigger').on('click', function (event) {
        event.preventDefault();
        $('.popup').addClass('is-visible');
    });

    //close popup
    $('.popup').on('click', function (event) {
        if ($(event.target).is('.popup-close') || $(event.target).is('.popup')) {
            event.preventDefault();
            $(this).removeClass('is-visible');
        }
    });
    //close popup when clicking the esc keyboard button
    $(document).keyup(function (event) {
        if (event.which == '27') {
            $('.popup').removeClass('is-visible');
        }
    });
});

// The class you click to trigger the popup 
$('.letspop').on('click', function () {
    // The Overlay fades in
    $(".overlay").fadeIn(200, function () { });
    // The Popup fades in just after
    $(".popup").fadeIn(600, function () { });
});

// The class's you click when you want to close the pop
$('.overlay, .close').on('click', function () {
    // The Overlay fades out slower
    $(".overlay").fadeOut(600, function () { });
    // The Popup fades out faster
    $(".popup").fadeOut(200, function () { });
});