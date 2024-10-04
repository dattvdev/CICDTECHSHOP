export default function methodGet(url, callBack) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            callBack(res)
        }
    });
}