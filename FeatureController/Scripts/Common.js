function formFailure(xhr) {
    var $form = $(this);
    $form.find('.form-group.has-error').removeClass('has-error');
    $form.find('.help-block[data-server-validation-for]')
        .removeClass('show')
        .text('');

    $form.find('[data-valmsg-summary="true"]').find('li').remove();

    if (xhr.status === 400) {
        var response = JSON.parse(xhr.responseText);
        var fields = Object.keys(response);

        fields.forEach(function (field) {
            if (response[field].Errors && response[field].Errors.length > 0) {

                var errors = extractErrorMessages(response[field].Errors);

                if (field !== "") {
                    $('[name=' + field + ']')
                        .closest('.form-group')
                        .addClass('has-error');


                    var errorText = errors.join('<br \>');

                    $('[data-server-validation-for="' + field + '"]')
                        .addClass('show')
                        .text(errorText);
                } else {
                    var errorText = "<li >" + errors.join("</li><li>") + "</li>";
                    $form.find('[data-valmsg-summary="true"]')
                        .removeClass('validation-summary-valid').addClass('validation-summary-errors')
                        .find('ul')
                            .append(errorText)
                }
            }
        });
    }
}

function extractErrorMessages(errors) {

    var errorText = $.map(errors, function (element) {
        if (element.ErrorMessage)
            return element.ErrorMessage;
    });
    return errorText;
}

function formSuccess() {
    var $form = $(this);
    $form.find('.form-group.has-error').removeClass('has-error');
    $form.find('.help-block[data-server-validation-for]')
        .removeClass('show')
        .text('')
    //.addClass('')
    ;
    $form.find('[data-valmsg-summary="true"]')
        .addClass('validation-summary-valid').removeClass('validation-summary-errors')
        .find('li').remove();
}