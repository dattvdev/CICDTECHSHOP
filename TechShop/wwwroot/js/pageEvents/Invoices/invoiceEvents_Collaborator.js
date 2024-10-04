import methodGet from "../../Services/methodGet.js"
import methodPost from "../../Services/methodPost.js"
import methodGetFile from "../../Services/methodGetFile.js"
import methodPostFile from "../../Services/methodPostFile.js"
import { showDataTable, showDataTableServerSide } from "../../Utilities/showDatatable.js"
import { timeNow } from "../../Utilities/handleTime.js"
$(document).ready(function () {
    const dataTable = $("#myTable");
    let url = "/AreaCollaborator/Invoice/GetInvoiceForCollaborator";
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
            { data: "status", title: "Status" },
            { data: "action", title: "Action"}
        ], true, false, false, (dataTableInstance, dataTable) => {
            dt = dataTableInstance
            const $dataTable = $(dataTable);
            dataTableInstance.on('draw', function () {
                $dataTable.find('.detail-btn').text('Update');
                $dataTable.find('.edit-btn').text('Take care!!');
            });

            dataTableInstance.off('click', '.detail-btn').on('click', '.detail-btn', handleClickButtonDetail);
            dataTableInstance.off('click', '.edit-btn').on('click', '.edit-btn', handleClickButtonEdit_takeCare);
        });
    }

    function handleClickButtonEdit_takeCare() {
        const id = $(this).data('id');
        var url = `/AreaCollaborator/Invoice/UpdateInvoiceTakeCare?id=${id}`
        methodGet(url, (response) => {
            if (response.isSuccess) {
                showAlertSuccess(response.message)
                dt.ajax.reload();
            }
            else {
                showAlertError(response.message)
            }
        })
    }

    function handleClickButtonDetail() {
        const id = $(this).data('id');
        var url = `/AreaCollaborator/Invoice/Update?id=${id}`
        window.location.href = url;
    }

    function showDataTableServerSide(url, dataTable, columnOptions, isAddBtnDetail = true, isAddBtnEdit = true, isAddBtnDelete = true, callBack) {
        const dataTableInstance = dataTable.DataTable({
            responsive: true,
            rowReorder: {
                selector: 'td:nth-child(2)'
            },
            processing: true,
            serverSide: true,
            destroy: true,
            select: true,
            ajax: {
                url: url,
                type: 'POST',
                dataFilter: function (data) {
                    var json = jQuery.parseJSON(data);
                    console.log(json)

                    json.recordsTotal = json.recordsTotal;
                    json.recordsFiltered = json.recordsFiltered;

                    json.data = json.data.map(function (item) {
                        item.action = `
                            <button class="btn btn-primary detail-btn" data-id="${item.id}">Detail</button>

                        `;
                        if (!item.whoTakeCare) {
                            item.action += `<button class="btn btn-success edit-btn" data-id="${item.id}">Take Care</button>`
                        }
                        return item;
                    });

                    console.log(json)

                    return JSON.stringify(json); 
                }
            },
            language: {
                search: "",
                searchPlaceholder: "Search...",
                infoFiltered: ""
            },
            columns: columnOptions,
            lengthMenu: [5, 10, 25, 50, 100]
        });

        callBack(dataTableInstance, dataTable)
    }

})