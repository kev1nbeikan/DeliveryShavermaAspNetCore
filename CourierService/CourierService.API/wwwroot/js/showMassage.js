function showSuccessMessage() {
    $('#success-message').removeClass('d-none');
    $('#success-message').fadeIn(500).delay(2000).fadeOut(500);
}
function showInfoMessage(text) {
    $('#success-message').removeClass('d-none');
    $('#success-details').text(text);
    $('#success-message').fadeIn(500).delay(2000).fadeOut(500);
}
function showErrorMessage(errorMessage) {
    $('#error-message').removeClass('d-none');
    $('#error-details').text(errorMessage);
    $('#error-message').fadeIn(500).delay(2000).fadeOut(500);
}
