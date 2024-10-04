import methodGet from "../../Services/methodGet.js"
import methodPost from "../../Services/methodPost.js"
import methodGetFile from "../../Services/methodGetFile.js"
import methodPostFile from "../../Services/methodPostFile.js"
import { showDataTable, showDataTableServerSide } from "../../Utilities/showDatatable.js"
import { timeNow } from "../../Utilities/handleTime.js"
$(document).ready(function () {
    const dataTable = $("#myTable");
    const select_invoiceStatus = $("#select_invoiceStatus")
    const exportAll = $("#exportAll")
    const select_InvoiceMonth = $("#select_InvoiceMonth")

    let url = "/Admin/Invoices/GetDataInvoices";
    let selectedMonth = null
    let itemSelected = null
    let dt = null
    let invoiceParamesterOption = {
        Search: null,
        Status: null,
        StartDate: null,
        EndDate: null
    }

    function setup() {
        handelShowTable(getInvoiceUrl(invoiceParamesterOption))
        $('input[name="daterange"]').daterangepicker({
            opens: 'left',
            autoUpdateInput: false,
            locale: {
                cancelLabel: 'Clear'
            },
            maxDate: moment(),
        }, function (start, end, label) {
            console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        });
    }
    setup()

    select_invoiceStatus.change(handleChangeInvoiceStatus)
    exportAll.on("click", handleClickExcelAll)
    $('input[name="daterange"]').on('apply.daterangepicker', onApplyDaterange);
    $('input[name="daterange"]').on('cancel.daterangepicker', onClearDaterange);

    function handelShowTable(url) {
        showDataTableServerSide(url, dataTable, [
            { data: "id", visible: false },
            { data: "customerName", title: "Customer Name" },
            { data: "createAt", title: "Created At" },
            { data: "status", title: "Status" }
        ], true, true, true, (dataTableInstance, dataTable) => {
            dt = dataTableInstance

            const $dataTable = $(dataTable);

            $('.dt-search input').change(function () {
                invoiceParamesterOption.Search = $(this).val()
            });

            dataTableInstance.on('draw', function () {
                $dataTable.find('.edit-btn').html("<i class='fa-solid fa-file-excel'></i>").removeClass('btn-primary').addClass('btn-success');
                $dataTable.find('.delete-btn').text("Cancel")
            });
            dataTableInstance.off('click', '.detail-btn').on('click', '.detail-btn', handleClickButtonDetail);
            dataTableInstance.off('click', '.delete-btn').on('click', '.delete-btn', hanldeClickButtonDelete_CancleInvoice);
            dataTableInstance.off('click', '.edit-btn').on('click', '.edit-btn', hanldeClickButtonEdit_ExportExcelSingleInvoice);
        });
    }

    function getInvoiceUrl(invoiceParamesterOption) {
        let { Status, StartDate, EndDate } = invoiceParamesterOption;

        let url = "/Admin/Invoices/GetDataInvoices";

        if (Status !== null) {
            url += `?status=${Status}`;
        }

        if (StartDate !== null || EndDate !== null) {
            url += Status === null ? '?' : '&';
            if (StartDate !== null) {
                url += `startDate=${StartDate}&`;
            }
            if (EndDate !== null) {
                url += `endDate=${EndDate}`;
            }

            url = url.endsWith('&') ? url.slice(0, -1) : url;
        }

        return url;
    }

    function handleClickExcelAll() {
        exportAll.html('Excel <div class="spinner-border text-primary" role="status"></div>')
        let link = '/Admin/Invoices/ExporterExcelAllInvoices'
        methodPostFile(link, invoiceParamesterOption, handleExportExcelSuccess, handleExportExcelFailed)
    }

    function handleExportExcelSuccess(data, status, xhr) {
        var filename = "";
        var disposition = xhr.getResponseHeader('Content-Disposition');
        if (disposition && disposition.indexOf('attachment') !== -1) {
            var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
            var matches = filenameRegex.exec(disposition);
            if (matches != null && matches[1]) {
                filename = matches[1].replace(/['"]/g, '');
            }
        }
        var link = document.createElement('a');
        var url = window.URL.createObjectURL(data);
        link.href = url;
        link.download = filename || 'invoices.xlsx';
        document.body.append(link);
        link.click();
        window.URL.revokeObjectURL(url);
        link.remove();

        exportAll.html('Excel <i class="fa-solid fa-file-excel"></i>')
    }

    function handleExportExcelFailed(xhr, status, error) {
        exportAll.html('Excel <i class="fa-solid fa-circle-exclamation text-danger"></i>')
    }

    function handleChangeInvoiceStatus() {
        itemSelected = Number(select_invoiceStatus.val())
        itemSelected = itemSelected == 4 ? null : itemSelected
        invoiceParamesterOption.Status = itemSelected
        dt.ajax.url(getInvoiceUrl(invoiceParamesterOption)).load();
    }

    function hanldeClickButtonEdit_ExportExcelSingleInvoice() {
        const id = $(this).data('id');
        $(this).html('<div class="spinner-border text-primary" role="status"></div>')
        let url = `/Admin/Invoices/ExportExcelForInvoice?id=${id}`
        methodGetFile(url, (data) => {
            var a = document.createElement('a');
            var url = window.URL.createObjectURL(data);
            a.href = url;
            a.download = `Invoice${timeNow()}.xlsx`;
            document.body.append(a);
            a.click();
            window.URL.revokeObjectURL(url);
            a.remove();
            $(this).html('<i class="fa-solid fa-file-excel"></i>')
        }, (err) => {
            $(this).html('<i class="fa-solid fa-circle-exclamation text-danger"></i>')
        })
    }

    function hanldeClickButtonDelete_CancleInvoice() {
        const id = $(this).data('id');
        $(this).html('<div class="spinner-border text-primary" role="status"></div>')
        let urlCancel = `/Admin/Invoices/CancelInvoice?id=${id}`
        methodGet(urlCancel, (data) => {
            if (data.isSuccess) {
                showAlertSuccess(data.message)
                dt.ajax.reload();
            } else {
                showAlertError(data.message)
            }
            $(this).text('Cancel')
        }, (err) => {
            $(this).text('Cancel')
        })
    }

    function handleClickButtonDetail() {
        const id = $(this).data('id');
        var url = `/Admin/Invoices/Detail?id=${id}`
        window.location.href = url;
    }

    function onApplyDaterange(ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
        invoiceParamesterOption.StartDate = picker.startDate.startOf('day').toISOString();
        invoiceParamesterOption.EndDate = picker.endDate.endOf('day').toISOString();
        dt.ajax.url(getInvoiceUrl(invoiceParamesterOption)).load();
    }

    function onClearDaterange(ev, picker) {
        $(this).val('');
        invoiceParamesterOption.StartDate = null;
        invoiceParamesterOption.EndDate = null;
        dt.ajax.url(getInvoiceUrl(invoiceParamesterOption)).load();
    }

})