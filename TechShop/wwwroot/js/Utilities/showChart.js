export function handleShowChartColumn(type, data, ctx, chart, isDisplayAxis_X = true, isDisplayAxis_y = true, isDisplayLable = true) {

    if (chart) {
        chart.destroy();
    }

    const labels = data.result.map(item => item.lable);

    const datasetsOption = [];

    for (let i = 0; i < data.dbSetLable.length; i++) {
        const dataKey = `data${i}`;
        datasetsOption.push({
            label: data.dbSetLable[i],
            data: data.result.map(item => item[dataKey]),
            borderWidth: 3,
            fill: false,
        });
    }

    chart = new Chart(ctx, {
        type: type,
        data: {
            labels: labels,
            datasets: datasetsOption,
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: { 
                    display: isDisplayAxis_y,
                    ticks: {
                        suggestedMin: 0,
                        beginAtZero: true
                    }
                },
                x: {
                    display: isDisplayAxis_X 
                }
            },
            plugins: {
                legend: { 
                    display: isDisplayLable
                }
            }
        },
    });

    return chart;
}


export function handleShowChartPieOrDoughnut(type, data, ctx, chart, isDisplayAxis_X = true, isDisplayAxis_y = true, isDisplayLable = true) {
    if (chart) {
        chart.destroy();
    }

    const labels = data.result.map(item => item.lable);

    const datasetsOption = [];

    for (let i = 0; i < data.dbSetLable.length; i++) {
        const dataKey = `data${i}`;
        const dataresults = data.result.map(item => item[dataKey])
        datasetsOption.push({
            label: data.dbSetLable[i],
            data: dataresults,
            borderWidth: 1,
        });
    }

    chart = new Chart(ctx, {
        type: type,
        data: {
            labels: labels,
            datasets: datasetsOption,
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    display: isDisplayAxis_y,
                    ticks: {
                        suggestedMin: 0,
                        beginAtZero: true
                    }
                },
                x: {
                    display: isDisplayAxis_X
                }
            },
            plugins: {
                legend: {
                    display: isDisplayLable
                }
            }
        },
    });

    return chart;
}
