export default function methodGetFile(url, onSuccess, onError) {
    $.ajax({
        url: url,
        type: 'GET',
        xhrFields: {
            responseType: 'blob'
        },
        success: function (data) {
            onSuccess(data)
        },
        error: function (err) {
            onError(err)
        }
    });
}