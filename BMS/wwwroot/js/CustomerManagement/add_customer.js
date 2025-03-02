$(document).ready(function () {
    $('form').submit(function (event) {
        event.preventDefault();  // 阻止表单提交

        var formData = new FormData(this);  // 获取表单数据

        $.ajax({
            url: addCustomerUrl,
            type: 'POST',
            data: formData,
            processData: false,  // 必须设置为 false，才能正确发送 FormData
            contentType: false,  // 必须设置为 false，才能正确发送 FormData
            success: function (response) {
                if (response.message) {
                    // 在页面上显示返回的消息
                    alert(response.message);
                }
                if (response.status == '200') {
                    window.location.href = CustomerListUrl;
                }
            },
            error: function () {
                alert('上传失败');
            }
        });
    });


});
