$(document).ready(function () {

    $("#bindOK").click(function () {
        var sendData = {};
        let projectIds = [];
        $("#checkboxContainer input[type='checkbox']:checked").each(function () {
            projectIds.push(parseInt($(this).val(), 10)); // 将值转换为整数
        });
        sendData.projectIds = projectIds;
        sendData.groupId = parseInt($("#selectedGroupId").val(), 10);
        $.ajax({
            url: bindProjectToGroupUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(sendData),
            success: function (response) {
                if (response.message) {
                    // 在页面上显示返回的消息
                    alert(response.message);
                    window.location.href = GroupIndexUrl;
                }
            },
            error: function (xhr, status, error) {
                alert('绑定失败');
            }
        });

    });

    // 监听模态框的显示事件
    $('#bindProjectModal').on('show.bs.modal', function (event) {
        // 获取触发按钮
        var button = $(event.relatedTarget);

        // 获取 data-* 属性中的值
        var groupId = button.data('id');
        $("#selectedGroupId").val(groupId);
        var groupName = button.data('name');
        var requestUrl = getAllProjectUrl + "?groupId=" + groupId;
        // 发送 AJAX 请求获取详细信息
        $.ajax({
            url: requestUrl, // 使用动态生成的请求 URL
            method: 'GET', // 请求方式
            success: function (response) {
                // 假设返回的数据结构是 { name: "电站名称", imageUrl: "图片链接" }
                $('#bindModalGroupName').text(groupName);
                // 确保 response 是数组
                if (Array.isArray(response)) {
                    let checkboxContainer = $("#checkboxContainer"); // 获取复选框容器
                    checkboxContainer.empty(); // 清空已有内容，避免重复加载

                    response.forEach(function (item) {
                        let isChecked = item.isSelected ? "checked" : "";
                        let checkboxHtml = `
                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" id="option${item.id}" value="${item.id}" ${isChecked}>
                        <label class="form-check-label" for="option${item.id}">${item.customerProjectName}</label>
                    </div>
                `;
                        checkboxContainer.append(checkboxHtml); // 添加到容器中
                    });
                } else {
                    console.log("返回数据格式错误:", response);
                }
            },
            error: function (error) {
                console.log('获取数据失败:', error);
            }
        });
        //// 更新模态框内容
        //$('#modal-user-id').text(userId);
        //$('#modal-user-name').text(decodeURIComponent(userName.replace(/\+/g, ' ')));
    });
});
