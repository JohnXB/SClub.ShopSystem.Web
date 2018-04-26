layui.use('layer', 'form', function () {
    var form = layui.form();
    var layer = layui.layer;


});
function ChangeInfo(InfoId) {
    var address = new Object();
    address.InfoId = InfoId;
    $.ajax({
        url: "/User/DeleteAddressInfo",
        data: { "address": address },
        type: "post",
        dataType: "json",
        error: function (xhr) {
            layer.msg('删除地址失败')
        },
        success: function (data) {
            if (data) {
                layer.open({
                    title: '结果'
                    , content: '删除地址成功'
                    , yes: function (index, layero) {
                        window.location.href = "/User/AddressInfo"
                    }
                    , cancel: function () {
                        window.location.href = "/User/AddressInfo"
                    }
                });
            }
            else {
                layer.msg('删除地址失败')
            }
        }
    })
}

function AddAddressBtn() {
    layui.use('form', function () {
        var form = layui.form();
        form.verify({

        });
    });

    var addressInfo = $("input[name='address']").val()
    var consignee = $("input[name='consignee']").val()
    var tel = $("input[name='tel']").val()
    if (addressInfo == "" || consignee == "" || tel == "" || !(/^1[34578]\d{9}$/.test(tel))) {
        return;
    }

    var address = new Object();
    address.Address = addressInfo;
    address.Consignee = consignee;
    address.Tel = tel;
    $.ajax({
        url: "/User/AddAddressInfo",
        data: { "address": address },
        type: "post",
        dataType: "json",
        error: function (xhr) {
            layer.msg('新增地址失败')
        },
        success: function (data) {
            if (data) {
                layer.open({
                    title: '结果'
                    , content: '新增地址成功'
                    , yes: function (index, layero) {
                        window.location.href = "/User/AddressInfo"
                    }
                    , cancel: function () {
                        window.location.href = "/User/AddressInfo"
                    }
                });
            }
            else {
                layer.msg('新增地址失败')
            }
        }
    })
}

function addModal() {
    layui.use('form', function () {
        var form = layui.form();
    });
    layer.open({
        title: "地址信息"
       , type: 1
       , content: $('#addAddress_Modal')
       , shade: 0.3
       , area: '500px'
       , btnAlign: 'c' //按钮居中
       , move: false
       , cancel: function () {
           $('#reset').click()
           document.getElementById('addAddress_Modal').style.display = "none"
       }
    });
}