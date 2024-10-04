export default function methodPost(url, dataOptions, onSuccess, onError) {
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        data: dataOptions,
        success: function (response) {
            onSuccess(response)
        },
        error: function (xhr, status, error) {
            onError(xhr, status, error)
        }
    });
}