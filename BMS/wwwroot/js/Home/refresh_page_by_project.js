
$(document).ready(function () {
    refreshPageByProject();
    $("#refreshPageByProjectButton").click(function () {
        refreshPageByProject();
    });
});

function refreshPageByProject() {
    let selectedProject = getSelectedProjectIds();
    if (selectedProject.length == 0) {
        alert("选中的 ID: " + selectedProject.join(", "));
        return;
    }

    refreshProjectOverview(selectedProject);
}

function refreshProjectOverview(selectedProjectIds) {
    $.ajax({
        url: GetProjectOverviewByProject,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(selectedProjectIds),
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

function getSelectedProjectIds() {
    let selectedIds = [];

    // 选择所有被选中的 .projectCheckbox
    $(".projectCheckbox:checked").each(function () {
        selectedIds.push($(this).val()); // 获取复选框的 value（即项目 ID）
    });

    return selectedIds; // 如果需要返回 ID 数组
}