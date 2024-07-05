function showSuccessMessage(text) {
    $('#success-message').removeClass('d-none');
    $('#success-message').text(text);
    $('#success-message').fadeIn(500).delay(2000).fadeOut(500);
}

function showErrorMessage(errorMessage) {
    $('#error-message').removeClass('d-none');
    $('#error-details').text(errorMessage);
    $('#error-message').fadeIn(500).delay(2000).fadeOut(500);
}
