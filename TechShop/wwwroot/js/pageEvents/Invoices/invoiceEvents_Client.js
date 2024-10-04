import methodGet from "../../Services/methodGet.js"
import methodPost from "../../Services/methodPost.js"
import methodGetFile from "../../Services/methodGetFile.js"
import methodPostFile from "../../Services/methodPostFile.js"
import { showDataTable, showDataTableServerSide } from "../../Utilities/showDatatable.js"
import { timeNow } from "../../Utilities/handleTime.js"
$(document).ready(function () {
    const dataTable = $("#myTable");

    let url = "/Customer/GetDataInvoices";
    let dt = null

    function setup() {
        handelShowTable(url)
    }
    setup()

    function handelShowTable(url) {
        showDataTableServerSide(url, dataTable, [
            { data: "id", visible: false },
            { data: "customerName", title: "Customer Name" },
            { data: "createAt", title: "Created At" },
            { data: "status", title: "Status" }
        ], true, false, false, (dataTableInstance, dataTable) => {
            dt = dataTableInstance

            dataTableInstance.off('click', '.detail-btn').on('click', '.detail-btn', handleClickButtonDetail);
        });
    }

    function handleClickButtonDetail() {
        const id = $(this).data('id');
        var url = `/Customer/Detail?id=${id}`
        window.location.href = url;
    }

})