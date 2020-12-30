function focusAction(controlToFocus) {
    $("html, body").animate({
        scrollTop: $("#" + controlToFocus).offset().top - 100
    }, 1000);
}