function ajaxCall(url, data, successFunction, errorFunction) {
    $.ajax({
        url: url,
        data: data,
        dataType: 'json',
        success: function (data, textStatus, jqXHR) {
            alert(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
            alert(thrownError);
        },
        type: 'GET'
    });
}