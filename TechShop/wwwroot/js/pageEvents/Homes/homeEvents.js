import methodGet from "../../Services/methodGet.js"
import countUpNumber from "../../Utilities/countUpNumber.js"
import { timeAgo } from "../../Utilities/handleTime.js"
import { handleShowChartColumn, handleShowChartPieOrDoughnut } from "../../Utilities/showChart.js"
$(document).ready(function () {
    const span_header_totalProduct = $("#total-product")
    const span_header_totalBrands = $("#total-brand")
    const span_header_total_customer = $("#total-customers")
    const span_header_total_Collaborators = $("#total-Collaborators")
    const chartInvoiceStatus = $("#chartInvoiceStatus")
    const chartProductSoldAndNotSold = $("#chartProductSoldAndNotSold")
    const chartSaleThisMonth = $("#chartSaleThisMonth")
    const chartTopSellingProduct = $("#chartTopSellingProduct")
    const tableCollaborator = $("#tableCollaborators")
    const activityNewInvoice = $("#activityNewInvoice")
    const activityTopRatedProduct = $("#activityTopRatedProduct")
    const activityTopCustomersHaveHighLoyaltyPoint = $("#activityTopCustomersHaveHighLoyaltyPoint")

    let currentChartInvoice = null
    let currentchartProductSoldAndNotSold = null
    let currentChartSaleThisMonth = null
    let currentChartTopSellingProduct = null


    function init() {
        methodGet("/Admin/Tracking/CountingStatistics", (data) => {
            countUpNumber(data.countProducts, span_header_totalProduct, 1000)
            //span_header_totalProduct.text(data.countProducts)
            countUpNumber(data.countBrands, span_header_totalBrands, 1000)
            //span_header_totalBrands.text(data.countBrands)
            countUpNumber(data.countCustomters, span_header_total_customer, 1000)
            //span_header_total_customer.text(data.countCustomters)
            countUpNumber(data.countCollaborators, span_header_total_Collaborators, 1000)
            //span_header_total_Collaborators.text(data.countCollaborators)

            activityNewInvoice.find("a").attr('href', `https://localhost:7061/Admin/Invoices/Detail?id=${data.customerJustOrdered.id}`);
            activityNewInvoice.find("span").eq(0).html("<strong>" + data.customerJustOrdered.customerName + "</strong>")
            activityNewInvoice.find("span").eq(1).html("just paid the bill for a total of <strong>" + data.customerJustOrdered.totalPrice + "$</strong>")
            activityNewInvoice.find("span").eq(2).html(timeAgo(data.customerJustOrdered.createAt))

            activityTopRatedProduct.find("a").attr('href', `https://localhost:7061/Admin/Product/Details?id=${data.topRateProduct.productId}`);
            activityTopRatedProduct.find("span").eq(0).html("<strong>" + data.topRateProduct.productName + "</strong>")
            activityTopRatedProduct.find("span").eq(1).html(" is rated highest with score <strong>" + data.topRateProduct.totalRate + "/5</strong>")

            activityTopCustomersHaveHighLoyaltyPoint.find("span").eq(0).html("<strong>" + data.cusotmerHighLoyaltyPoint.userName + "</strong>")
            activityTopCustomersHaveHighLoyaltyPoint.find("span").eq(1).html("Have highest score <strong>" + data.cusotmerHighLoyaltyPoint.loyaltyPoint + "</strong>")
        })

        methodGet("/Admin/Tracking/DAChartInvoices", (data) => {
            currentChartInvoice = handleShowChartColumn("doughnut", data, chartInvoiceStatus, currentChartInvoice, false, false, false)
        })

        methodGet("/Admin/Tracking/DAChartProductSoldAndNotSold", (data) => {
            currentchartProductSoldAndNotSold = handleShowChartColumn("bar", data, chartProductSoldAndNotSold, currentchartProductSoldAndNotSold, false, true, false)
        })

        methodGet("/Admin/Tracking/DAChartProductSoldInMonth", (data) => {
            currentChartSaleThisMonth = handleShowChartColumn("line", data, chartSaleThisMonth, currentChartSaleThisMonth, true, false, true)
        })

        methodGet("/Admin/Tracking/DAChartTopSellingProducts", (data) => {
            currentChartTopSellingProduct = handleShowChartColumn("bar", data, chartTopSellingProduct, currentChartTopSellingProduct, true, true, true)
        })

        methodGet("/Admin/Tracking/GetCollaboratorsInDashBoards", hanldShowTableCollaborators)

    }

    init();

    function hanldShowTableCollaborators(data) {
        console.log(data)
        tableCollaborator.empty();
        data.forEach((item, index) => {
            let checked = item.isActive ? "<i class='fa-solid fa-circle text-primary'></i>" : "<i class='fa-solid fa-circle text-danger'></i>"
            tableCollaborator.append(`
                             <tr>
                                <td class="border-top-0 py-4">
                                    <div class="d-flex no-block align-items-center">
                                        <div class="">
                                            <h5 class="text-dark mb-0 font-16 font-weight-medium">
                                               ${item.name}
                                            </h5>
                                            <span class="text-muted font-14">
                                                ${item.email}
                                            </span>
                                        </div>
                                    </div>
                                </td>
                                <td class="border-top-0 text-muted py-4 font-14 text-center">
                                    ${checked}
                                </td>
                                <td class="border-top-0 px-2 py-4 text-center">
                                    ${item.dayActive}
                                </td> 
                             </tr>
                `)
        })
    }

})