$('#importBtn').on('click', function () {
    $('#fileInput').click();
});


// 当文件选择发生变化时，上传文件
$('#fileInput').on('change', function (e) {
    var file = e.target.files[0];

    // 如果没有选择文件，则直接返回
    if (!file) {
        alert("请先选择文件！");
        return;
    }

    var formData = new FormData();
    formData.append("file", file); // 将文件附加到 FormData

    // 使用 jQuery 的 Ajax 来上传文件
    $.ajax({
        url: ImportDeviceInfoUrl, 
        type: 'POST',
        data: formData,
        contentType: false, // 不设置 contentType，jQuery 会自动处理
        processData: false, // 不处理数据
        success: function (data) {
            if (data.success) {
                alert('设备导入成功！');
                window.location.href = deviceListUrl;
            } else {
                alert('导入失败：' + data.message);
            }
        },
        error: function (xhr, status, error) {
            alert('上传失败，请重试');
            console.error('错误:', error);
        }
    });
});