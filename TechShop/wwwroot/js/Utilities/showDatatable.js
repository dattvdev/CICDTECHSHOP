import { insertSpaceBeforeUppercase } from "../Utilities/handleString.js"

export function showDataTable(data, dataTable, option , callBack = null) {

    const theader = dataTable.find("thead tr").empty()
    var headers = Object.keys(data[0]).map(key => {
        return {
            data: key
        }
    });

    headers.forEach((item) => {
        let editheader = item.data.charAt(0).toUpperCase() + item.data.slice(1)
        editheader = insertSpaceBeforeUppercase(editheader)
        theader.append(`
                <th>
                    ${editheader}
                </th>
            `)
    })

    option.data = data
    option.columns = headers
    option.responsive = true
    option.rowReorder = {
        selector: 'td:nth-child(2)'
    }

    const dataTableInstance = dataTable.DataTable(option);

    if (callBack != null) {
        callBack(dataTableInstance)
    }
}

export function showDataTableServerSide(url, dataTable, columnOptions, isAddBtnDetail = true, isAddBtnEdit = true, isAddBtnDelete = true, callBack) {
    
    if (isAddBtnDetail == true || isAddBtnEdit == true || isAddBtnDelete == true) {
        columnOptions.push({
            data: null,
            orderable: false,
            className: "text-center",
            render: function (data, type, row) {
                let buttons = '';
                if (isAddBtnDetail) {
                    buttons += `<button class="btn btn-info detail-btn" data-id="${row.id}">Detail</button> `;
                }
                if (isAddBtnEdit) {
                    buttons += `<button class="btn btn-primary edit-btn" data-id="${row.id}">Edit</button> `;
                }
                if (isAddBtnDelete) {
                    buttons += `<button class="btn btn-danger delete-btn" data-id="${row.id}">Delete</button>`;
                }
                return buttons.trim();
            }
        });
    }

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
            type: 'POST'
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
