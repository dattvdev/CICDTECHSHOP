import methodGet from "../../Services/methodGet.js"
import methodPost from "../../Services/methodPost.js"
import { handleShowChartColumn } from "../../Utilities/showChart.js"
$(document).ready(function () {
    const container_item_selected = $("#container-item-selected")
    const container_item = $("#container-item")
    const loading_search = $(".loading")
    const input_search = $("#search")
    const input_select_date = $("#select-date")
    const option_ChartType = $("#option-chart-type")
    const option_itemType = $("#option-item-type")
    const option_chart_size = $("#option-chart-size")
    const option_page_size_item_type = $("#option-page-size-item-type")
    const option_hot_recommends = $("#option-hot-recommends")
    const btn_prev_container_item = $("#btn-prev-container-item")
    const btn_next_container_item = $("#btn-next-container-item")
    const main_chart = $("#main-Chart")
    let debounce = null
    let currentChart = null

    const optionShowChart = {
        chartType: 'bar',
        itemType: 'oVotes',
        searchItem: '',
        pageSizeItemType: 5,
        currentPageItemType: 1,
        chartSize: 0,
        startDate: null,
        endDate: null,
        itemSelected: [],
        option: {
            aaa: 111,
            bbb: 22
        }
    }

    function setup() {
        loading_search.hide()
        btn_prev_container_item.hide()
        input_select_date.daterangepicker({
            opens: 'left',
            autoUpdateInput: false,
            locale: {
                cancelLabel: 'Clear'
            },
            maxDate: moment(),
        }, function (start, end, label) {
            console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
        });
        getDataItem()
        getDataChart()
    }
    setup()

    input_search.on('keyup', handleSearchItem)
    input_select_date.on('apply.daterangepicker', onApplyDateRange);
    input_select_date.on('cancel.daterangepicker', onCancelDateRange);
    option_ChartType.on('change', handleChangeChartType)
    option_itemType.on('change', hanldeChangeItemType)
    option_chart_size.on('change', hanldeChangeChartSize)
    option_page_size_item_type.on('change', hanldeChangePageSizeItemType)
    btn_prev_container_item.on('click', onClickButtonPrev)
    btn_next_container_item.on('click', onClickButtonNext)

    function onApplyDateRange(ev, picker) {
        let daterange = $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
        optionShowChart.startDate = picker.startDate.format('MM/DD/YYYY')
        optionShowChart.endDate = picker.endDate.format('MM/DD/YYYY')
        if (optionShowChart.itemType == "oInvoiceDates") {
            getDataChart()
        }
    }

    function onCancelDateRange(ev, picker) {
        let cancleDateRange = $(this).val('');
        optionShowChart.startDate = null
        optionShowChart.endDate = null
        if (optionShowChart.itemType == "oInvoiceDates") {
            getDataChart()
        }
    }

    function hanldeChangeItemType() {
        optionShowChart.currentPageItemType = 1
        optionShowChart.itemType = option_itemType.val()
        optionShowChart.itemSelected = []
        getDataItem()
        loadContainerItemSelected()
    }

    function hanldeChangeChartSize() {
        optionShowChart.chartSize = Number(option_chart_size.val())
        getDataChart()
    }

    function handleChangeChartType() {
        optionShowChart.chartType = option_ChartType.val()
        getDataChart()
    }

    function handleSearchItem() {
        loading_search.show()
        clearTimeout(debounce)
        debounce = setTimeout(function () {
            optionShowChart.searchItem = input_search.val()
            getDataItem()
            loading_search.hide()
        }, 500)
    }

    function handleGetItemSuccess(response) {
        if (response.currentPage == 1)
            btn_prev_container_item.hide()
        if (response.totalPage == optionShowChart.currentPageItemType) {
            btn_next_container_item.hide()
        }
        else if (response.totalPage < optionShowChart.currentPageItemType) {
            if (response.totalPage != 0) {
                optionShowChart.currentPageItemType = response.totalPage
                btn_next_container_item.hide()
                getDataItem()
            }
        } else {
            btn_next_container_item.show()
        }

        handleLoadHotRecommend(response.recommend)
        handleLoadContainerItem(response)
    }
    function handleLoadHotRecommend(recommend) {
        option_hot_recommends.empty()
        if (recommend == null)
        {
            return
        }
        if (optionShowChart.itemType == "oSells" || optionShowChart.itemType == "oVotes" || optionShowChart.itemType == "oCustomerLoyal") {
            option_hot_recommends.append(`<option class="item-hot-recommend" value="${recommend.highest.id}">${recommend.highest.name} highest</option>`)
            option_hot_recommends.append(`<option class="item-hot-recommend" value="${recommend.lowest.id}">${recommend.lowest.name} lowest</option>`)
            option_hot_recommends.off('change').on('change', handleHotRecommendForObject);
        }
        else if (optionShowChart.itemType == "oInvoiceDates") {
            recommend.forEach((item, index) => {
                option_hot_recommends.append(
                    `<option class="item-hot-recommend" value="${item.dateRecommend}">${item.dateRecommend} most sold with ${item.product_best_seller_In_Date.name} highest</option>`
                )
                option_hot_recommends.off('change').on('change', handleHotRecommendForArray)
            })
        }
    }

    function handleHotRecommendForArray() {
        let selectedDate = $(this).val();
        let parsedDate = moment(selectedDate, "MM/DD/YYYY");
        let endDate = parsedDate.clone().add(1, 'days');
        let startDate = parsedDate.clone().subtract(1, 'days');
        input_select_date.data('daterangepicker').setStartDate(startDate);
        input_select_date.data('daterangepicker').setEndDate(endDate);
        input_select_date.val(startDate.format('MM/DD/YYYY') + '-' + endDate.format('MM/DD/YYYY'));
        optionShowChart.startDate = startDate.format('MM/DD/YYYY')
        optionShowChart.endDate = endDate.format('MM/DD/YYYY')
        getDataChart()
    }

    function handleHotRecommendForObject() {
        let selectedOption = $(this).find('option:selected');
        let id = selectedOption.val();
        let name = selectedOption.text();

        if (!Array.isArray(optionShowChart.itemSelected)) {
            optionShowChart.itemSelected = [];
        }

        let exists = optionShowChart.itemSelected.some(item => item.id === id);

        if (!exists) {
            optionShowChart.itemSelected.unshift({
                id: id,
                name: name
            });
            getDataItem()
            loadContainerItemSelected()
        }
    }

    function handleChartProduct_Success(response) {
        handleLoadChart(response)
    }

    function handleGetItemFailed(xhr, status, error) {
        console.log("Error " + status)
        console.log(xhr)
        console.log(error)
    }

    function handleLoadContainerItem(response) {
        container_item.empty()
        response.data.forEach((item, index) => {
            container_item.append(`<li class="list-group-item list-group-item-action btn item" data-id=${item.id}>${item.name}</li>`)
        })

        $(".item").draggable({
            cursor: "pointer",
            revert: true,
            helper: "clone",
            appendTo: "body",
            start: handleDragItem_Start,
            drag: handleDragItem_drag
        });

        $(".item").off('click').on('click', handleClickItem)

        container_item_selected.droppable({
            accept: '.item',
            classes: {
                'ui-droppable-hover': 'highlight'
            },
            drop: handleDropItem
        })
    }

    function handleDragItem_Start(event, ui) {
        $(ui.helper).css({
            'z-index': 100000,
            'width': $(this).width(),
            'background-color': $(this).css('background-color'),
            'border-radius': $(this).css('border-radius'),
            'padding': $(this).css('padding'),
            'margin': $(this).css('margin'),
            '-moz-box-shadow': '0 0 .5em rgba(0, 0, 0, .8)',
            '-webkit - box - shadow': '0 0 .5em rgba(0, 0, 0, .8)',
            'box-shadow': '0 0 .5em rgba(0, 0, 0, .8)'
        });
    }

    function handleDragItem_drag(event, ui) {
        ui.position.top = event.clientY - $(ui.helper).height() / 2;
        ui.position.left = event.clientX - $(ui.helper).width() / 2;
    }

    function handleDropItem(event, ui) {
        let id = ui.draggable.data('id');
        let name = ui.draggable.text()
        let targetOffset = $(this).offset();
        let itemOffset = ui.draggable.offset();
        let moveTop = targetOffset.top - itemOffset.top;
        let moveLeft = targetOffset.left - itemOffset.left;

        ui.draggable.draggable('disable');
        ui.draggable.draggable('option', 'revert', false);
        ui.draggable.animate({
            transform: `translateY(-100 %)`,
            opacity: `0`
        }, 200, function () {
            ui.draggable.remove();
            if (Array.isArray(optionShowChart.itemSelected)) {
                optionShowChart.itemSelected.unshift({
                    id: id,
                    name: name
                })
            } else {
                optionShowChart.itemSelected = []
                optionShowChart.itemSelected.unshift(id)
            }
            getDataItem()
            loadContainerItemSelected()
        });
    }

    function handleClickItem() {
        let id = $(this).data('id')
        let name = $(this).text()
        if (Array.isArray(optionShowChart.itemSelected)) {
            optionShowChart.itemSelected.unshift({
                id: id,
                name: name
            })
        } else {
            optionShowChart.itemSelected = []
            optionShowChart.itemSelected.unshift({
                id: id,
                name: name
            })
        }
        getDataItem()
        loadContainerItemSelected()
    }

    function loadContainerItemSelected() {
        container_item_selected.empty()
        optionShowChart.itemSelected.forEach((item, index) => {
            container_item_selected.append(`<button class="btn btn-outline-dark p-1 m-1 rounded-3 animate__animated animate__bounceIn item-selected" data-id=${item.id}>
                                       <i class="fa-solid fa-circle-xmark"></i>
                                        ${item.name}
                                    </button>`)
        })
        getDataChart()
        $('.item-selected').off('click').on('click', hanldeClickOnItemSelected)
    }

    function getDataChart() {
        if (optionShowChart.itemType == "oInvoiceDates" && optionShowChart.startDate != null && optionShowChart.endDate != null) {
            methodPost("/Admin/TrackingData/DAChartProductWithDateRange", optionShowChart, handleChartProduct_Success, handleGetItemFailed)
        } else {
            methodPost("/Admin/TrackingData/DAChartData", optionShowChart, handleShowChart_Success, handleGetItemFailed);
        }
    }

    function handleShowChart_Success(response) {
        handleLoadChart(response)
    }
    function handleLoadChart(data) {
        currentChart = handleShowChartColumn(optionShowChart.chartType, data, main_chart, currentChart)
    }

    function hanldeClickOnItemSelected() {
        var id = $(this).data('id')
        optionShowChart.itemSelected = optionShowChart.itemSelected.filter(i => i.id != id)
        getDataItem()
        loadContainerItemSelected()
    }

    function getDataItem() {
        methodPost("/Admin/TrackingData/GetItem", optionShowChart, handleGetItemSuccess, handleGetItemFailed);
    }

    function onClickButtonPrev() {
        var currentPage = Number(optionShowChart.currentPageItemType)
        btn_next_container_item.show()
        if (currentPage == 1)
            return
        if (currentPage == 2) {
            btn_prev_container_item.hide()
        }
        optionShowChart.currentPageItemType = currentPage - 1
        getDataItem()

    }

    function onClickButtonNext() {
        var currentPage = Number(optionShowChart.currentPageItemType)
        if (currentPage >= 1)
            btn_prev_container_item.show()
        optionShowChart.currentPageItemType = currentPage + 1
        getDataItem()
    }

    function hanldeChangePageSizeItemType() {
        var pageSize = option_page_size_item_type.val()
        optionShowChart.pageSizeItemType = pageSize
        getDataItem()
    }
})