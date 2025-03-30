$(document).ready(function () {
    // ECharts 配置
    var chartDom = document.getElementById('chart_charge_discharge');
    var myChart = echarts.init(chartDom);
    var option = {
        tooltip: {
            trigger: 'axis',  // 鼠标悬停时显示轴上信息
        },
        legend: {
            data: ['充电', '放电'],  // 图例显示充电与放电
            top: '5%',
            left: 'center'
        },
        xAxis: {
            type: 'category',  // X轴为类目轴
            boundaryGap: false,  // 不让折线图与边界重叠
            data: ['00:00', '01:00', '02:00', '03:00', '04:00', '05:00', '06:00', '07:00', '08:00', '09:00', '10:00', '11:00', '12:00']  // 时间轴
        },
        yAxis: {
            type: 'value',  // Y轴为数值轴
            axisLabel: {
                formatter: '{value} %'  // 格式化 Y 轴标签
            }
        },
        series: [
            {
                name: '充电',
                type: 'line',  // 类型为折线图
                data: [0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 100, 100],  // 充电数据
                smooth: true,  // 平滑曲线
                lineStyle: {
                    color: '#33CC33',  // 充电线条颜色
                    width: 3
                },
                symbol: 'circle',  // 数据点样式为圆形
                symbolSize: 8,  // 数据点大小
            },
            {
                name: '放电',
                type: 'line',  // 类型为折线图
                data: [100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 0, 0, 0],  // 放电数据
                smooth: true,  // 平滑曲线
                lineStyle: {
                    color: '#FF3333',  // 放电线条颜色
                    width: 3
                },
                symbol: 'circle',  // 数据点样式为圆形
                symbolSize: 8,  // 数据点大小
            }
        ]
    };

    // 使用指定的配置项和数据显示图表
    myChart.setOption(option);
});
