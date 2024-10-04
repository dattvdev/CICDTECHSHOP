export default function methodPostFile(url, data, onSuccess, onError) {
    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data), 
        xhrFields: {
            responseType: 'blob'  
        },
        success: function (data, status, xhr) {
            onSuccess(data, status, xhr)
        },
        error: function (xhr, status, error) {
            onError(xhr, status, error)
           
        }
    });
}
