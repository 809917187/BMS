document.addEventListener("DOMContentLoaded", function () {
    initChart('chart_battery_voltage', '电池单体电压');
    initChart('chart_cell_temperture', '电池单体温度');
});


function initChart(chart_id, title) {
    // 获取 DOM 元素并从 data-* 属性中提取数据
    var chartElement = document.getElementById(chart_id);
    var valuesData = JSON.parse(chartElement.getAttribute('data-values'));
    var countData = parseInt(chartElement.getAttribute('data-count'), 10);
    valuesData = valuesData.slice(0, countData);

    // 获取 x 轴数据（即数组下标）
    const xAxisData = valuesData.map((value, index) => index);
    const yAxisData = valuesData;  // 纵轴数据为 aaa 数组的值

    // 初始化 ECharts 实例
    var chart = echarts.init(chartElement);

    // 配置 ECharts 的 option
    var option = {
        title: {
            text: title, // 设置标题文本
            textAlign: 'center', // 标题居中
            left: 'center',      // 保证标题居中，`left: 'center'` 强制标题居中

        },
        tooltip: {
            trigger: 'axis'
        },
        xAxis: {
            type: 'category',
            data: xAxisData  // 横轴为数组下标
        },
        yAxis: {
            type: 'value'
        },
        series: [
            {
                data: yAxisData, // 纵轴为数组值
                type: 'line',     // 设置为折线图
                smooth: true       // 折线平滑
            }
        ]
    };

    // 使用指定的配置项和数据显示图表
    chart.setOption(option);
}