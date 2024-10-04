import { showDataTable } from "../../Utilities/showDatatable.js"
import methodGetFile from "../../Services/methodGetFile.js"
import { handleShowChartColumn } from "../../Utilities/showChart.js"
import { timeNow } from "../../Utilities/handleTime.js"
import methodGet from "../../Services/methodGet.js";
$(document).ready(function () {
    var url = window.location.href;
    var urlObj = new URL(url);
    var params = new URLSearchParams(urlObj.search);
    var id = params.get('id');


    const tableProducts = $("#tableProduct")

    const chartShoppingInMonth = $("#chartShoppingInMonth");
    let userId = chartShoppingInMonth.data("user-id");
    let currentChart = null

    methodGet(`/Customer/GetProductInInvoice?id=${id}`, (data) => {
        console.log(data)
        let totalPrice = data.reduce((total, currentValue) => total + (currentValue.productPrice * currentValue.quanity), 0)
        let totalQuanity = data.reduce((total, currentValue) => total + currentValue.quanity, 0)
        totalPrice = parseFloat(totalPrice.toFixed(2));

        let newItemTotal =
        {
            productName: "<strong>Total Price<strong>",
            quanity: `<strong>${totalQuanity}<strong>`,
            productPrice: `<strong>${totalPrice}$<strong>`,
        }

        data = data.map(currentValue => ({
            productName: `<a href='https://localhost:7061/Customer/Product/Details?id=${currentValue.id}'>` + currentValue.productName + "</a>",
            quanity: currentValue.quanity,
            productPrice: `${currentValue.productPrice}$`,
        }));

        data.push(newItemTotal)

        showDataTable(data, tableProducts,
            {
                searching: false,
                paging: false,
                info: false
            })
    })

});