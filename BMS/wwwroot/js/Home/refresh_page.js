
$(document).ready(function () {
    refreshPage(1);

    $("#refreshPageByProject").click(function () {
        refreshPage(1);
    });
    $("#refreshPageByCustomer").click(function () {
        refreshPage(2);
    });
    $("#refreshPageByGroup").click(function () {
        refreshPage(3);
    });

    $("#table_devices").bootstrapTable({
        pagination: true,  // 启用分页
        search: true,  // 启用搜索
        pageSize: 10,  // 默认每页10条
        pageList: [10, 20, 50, 100],  // 可选分页大小
    });
});

function refreshPage(type) {
    let selectedProject = getSelectedProjectIds(type);
    if (selectedProject.length == 0) {
        alert("未选中" + selectedProject.join(", "));
        return;
    }

    refreshProjectOverview(selectedProject, type);
    refreshWorkingStatusChart(selectedProject, type);
    refreshSOCStatusChart(selectedProject, type);
    refreshAlarmStatusChart(selectedProject, type);
    refresh24HoursStatusChart(selectedProject, type);
    refreshDeviceListTable(selectedProject, type);
}

function refreshDeviceListTable(selectedProjectIds, type) {
    $.ajax({
        url: GetDeviceList,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: getJsonDataForPageRefresh(selectedProjectIds, type),
        success: function (data) {
            data.forEach((item, i) => {
                item.index = i + 1;
            });
            $("#table_devices").bootstrapTable("load", data); // 加载数据
        },
        error: function (xhr, status, error) {
            console.error("获取设备数据失败:", error);
        }
    });
}

function linkFormatter(value, row, index) {
    if (!value) return "-";
    return `<a href="/Home/DeviceDetails?sn=${value}" target="_blank">${value}</a>`;
}


function refresh24HoursStatusChart(selectedProjectIds, type) {
    $.ajax({
        url: GetLast24HourStatusChart,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: getJsonDataForPageRefresh(selectedProjectIds, type),
        success: function (data) {
            let hours = data.map(d => d.hour);
            let charging = data.map(d => d.charging);
            let discharging = data.map(d => d.discharging);
            let idle = data.map(d => d.idle);
            let offline = data.map(d => d.offline);

            let chart = echarts.init(document.getElementById('chart_24_hours_status'));

            let option = {
                title: { text: '最近24小时状态统计' },
                tooltip: { trigger: 'axis', axisPointer: { type: 'shadow' } },
                legend: { data: ['充电', '放电', '空闲', '离线'] },
                xAxis: { type: 'category', data: hours, axisLabel: { rotate: 45 } },
                yAxis: { type: 'value' },
                series: [
                    { name: '充电', type: 'bar', stack: 'total', data: charging },
                    { name: '放电', type: 'bar', stack: 'total', data: discharging },
                    { name: '空闲', type: 'bar', stack: 'total', data: idle },
                    { name: '离线', type: 'bar', stack: 'total', data: offline }
                ]
            };

            chart.setOption(option);
        },
        error: function (xhr, status, error) {
            console.error("获取Alarm数据失败:", error);
        }
    });
}

function refreshAlarmStatusChart(selectedProjectIds, type) {
    $.ajax({
        url: GetAlarmStatusChart,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: getJsonDataForPageRefresh(selectedProjectIds, type),
        success: function (response) {
            // 解析数据，转换为 ECharts 饼状图格式
            var chartData = response.map(item => ({
                name: item.status,  // 状态名称
                value: item.count   // 对应的数量
            }));
            // 渲染 ECharts 饼状图
            var chartDom = document.getElementById("chart_alarm_status");
            if (!chartDom) {
                console.error("chart_alarm_status 容器未找到");
                return;
            }
            var myChart = echarts.init(chartDom);
            var option = {
                title: {
                    text: "Alarm",
                    left: "center"
                },
                tooltip: {
                    trigger: "item"
                },
                legend: {
                    orient: "vertical",
                    left: "left"
                },
                series: [
                    {
                        name: "Alarm",
                        type: "pie",
                        radius: "50%",
                        data: chartData,
                        emphasis: {
                            itemStyle: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: "rgba(0, 0, 0, 0.5)"
                            }
                        }
                    }
                ]
            };
            myChart.setOption(option);
        },
        error: function (xhr, status, error) {
            console.error("获取Alarm数据失败:", error);
        }
    });
}

function refreshSOCStatusChart(selectedProjectIds, type) {
    $.ajax({
        url: GetSOCStatusChart,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: getJsonDataForPageRefresh(selectedProjectIds, type),
        success: function (response) {
            // 解析数据，转换为 ECharts 饼状图格式
            var chartData = response.map(item => ({
                name: item.status,  // 状态名称
                value: item.count   // 对应的数量
            }));
            // 渲染 ECharts 饼状图
            var chartDom = document.getElementById("chart_soc");
            if (!chartDom) {
                console.error("chart_soc 容器未找到");
                return;
            }
            var myChart = echarts.init(chartDom);
            var option = {
                title: {
                    text: "SOC",
                    left: "center"
                },
                tooltip: {
                    trigger: "item"
                },
                legend: {
                    orient: "vertical",
                    left: "left"
                },
                series: [
                    {
                        name: "SOC",
                        type: "pie",
                        radius: "50%",
                        data: chartData,
                        emphasis: {
                            itemStyle: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: "rgba(0, 0, 0, 0.5)"
                            }
                        }
                    }
                ]
            };
            myChart.setOption(option);
        },
        error: function (xhr, status, error) {
            console.error("获取SOC数据失败:", error);
        }
    });
}

function refreshWorkingStatusChart(selectedProjectIds, type) {
    $.ajax({
        url: GetWorkingStatusChart,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: getJsonDataForPageRefresh(selectedProjectIds, type),
        success: function (response) {
            // 解析数据，转换为 ECharts 饼状图格式
            var chartData = response.map(item => ({
                name: item.status,  // 状态名称
                value: item.count   // 对应的数量
            }));
            // 渲染 ECharts 饼状图
            var chartDom = document.getElementById("chart_working_status");
            if (!chartDom) {
                console.error("chart_working_status 容器未找到");
                return;
            }
            var myChart = echarts.init(chartDom);
            var option = {
                title: {
                    text: "工作状态分布",
                    left: "center"
                },
                tooltip: {
                    trigger: "item"
                },
                legend: {
                    orient: "vertical",
                    left: "left"
                },
                series: [
                    {
                        name: "工作状态",
                        type: "pie",
                        radius: "50%",
                        data: chartData,
                        emphasis: {
                            itemStyle: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: "rgba(0, 0, 0, 0.5)"
                            }
                        }
                    }
                ]
            };
            myChart.setOption(option);
        },
        error: function (xhr, status, error) {
            console.error("获取工作状态数据失败:", error);
        }
    });
}

function getJsonDataForPageRefresh(selectedProjectIds, type) {
    return JSON.stringify({
        selectedIds: selectedProjectIds,
        type: parseInt(type, 10)
    });
}

function refreshProjectOverview(selectedProjectIds, type) {
    $.ajax({
        url: GetProjectOverview,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: getJsonDataForPageRefresh(selectedProjectIds, type),
        success: function (data) {
            $("#project_total_count").text(data.porjectCount);
            $("#device_total_count").text(data.totalDeviceCount);

            // 在这里更新页面上的内容，例如填充表格或更新 DOM
        },
        error: function (xhr, status, error) {
            console.error("获取项目概览失败:", error);
            alert("数据加载失败！");
        }
    });
}

function getSelectedProjectIds(type) {
    let selectedIds = [];

    if (type == 1) {
        // 选择所有被选中的 .projectCheckbox
        $(".projectCheckbox:checked").each(function () {
            selectedIds.push($(this).val());
        });
    } else if (type == 2) {
        $(".customerCheckbox:checked").each(function () {
            selectedIds.push($(this).val());
        });
    } else if (type == 3) {
        $(".groupCheckbox:checked").each(function () {
            selectedIds.push($(this).val());
        });
    }


    return selectedIds; // 如果需要返回 ID 数组
}