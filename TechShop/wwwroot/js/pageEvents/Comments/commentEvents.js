import methodGet from "../../Services/methodGet.js"
import methodPost from "../../Services/methodPost.js"
import { showDataTable, showDataTableServerSide } from "../../Utilities/showDatatable.js"
$(document).ready(function () {
    const dataTable = $("#myTable");
    const select_productId = $("#select_productId")
    const confirmDeleteModal = $("#confirmDeleteModal")



    let url = "/Admin/Comments/GetDataComments"
    let dt = null;

    handelShowTable(url)

    select_productId.change(function () {
        url = select_productId.val() == "all" ? "/Admin/Comments/GetDataComments" : `/Admin/Comments/GetDataComments?productId=${select_productId.val()}`
        if (dt != null) { 
            dt.ajax.url(url).load();
        }
    });


    function handelShowTable(url) {
        showDataTableServerSide(url, dataTable, [
            { data: "id", visible: false },
            { data: "user", title: "Customer Name" },
            { data: "commentAt", title: "Comment At" },
            { data: "rate", title: "Rate" },
            { data: "createAt", title: "CreateAt" }
        ], true, false, true, (dataTableInstance) => {
            dt = dataTableInstance
            dataTableInstance.on('click', '.delete-btn', function () {
                const _id = $(this).data('id');
                confirmDeleteModal.modal('show');

                $("#confirmDeleteButton").off("click").on("click", function () {
                    methodPost("/Admin/Comments/Delete", { id: _id },
                        (response) => {
                            if (response.success) {
                                showAlertSuccess(response.message, 3000)
                                if (dt != null) {
                                    dt.ajax.reload();
                                }
                            } else {
                                showAlertError(response.message, 3000);
                            }
                        },
                        (xhr, status, error) => {
                            showAlertError(xhr.responseText, 3000)
                        }
                    )
                    confirmDeleteModal.modal('hide');
                });
            });


            dataTableInstance.on('click', '.detail-btn', function () {
                const id = $(this).data('id');
                console.log('Detail button clicked for ID:', id);
                var url = `/Admin/Comments/Detail?id=${id}`
                window.location.href = url;
            });
        });
    }
})