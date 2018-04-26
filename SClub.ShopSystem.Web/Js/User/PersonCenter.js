
layui.use('element', function () {
    var element = layui.element(); //导航的hover效果、二级菜单等功能，需要依赖element模块
});
layui.use('layer', function () {
    var layer = layui.layer;
});



//修改图片
layui.use('upload', function () {
    layui.upload({
        url: '/User/ChangeUserImg'
      , elem: '#test' //指定原始元素，默认直接查找class="layui-upload-file"
      , method: 'post' //上传接口的http类型
      , before: function (input) {
          //返回的参数item，即为当前的input DOM对象
          console.log('文件上传中');
      }
      , success: function (res) {
          LAY_demo_upload.src = JSON.parse(res);
          UserImg.src = JSON.parse(res);
      }

    });
});

function ChangeNickName() {
    layer.open({
        title: "修改昵称"
        , type: 1
        , content: $('#ChangeNickName')
        , shade: 0.3
        , btnAlign: 'c' //按钮居中
        , move: false
        , cancel: function () {
            $("input[name='nickName']").val("")
            document.getElementById('ChangeNickName').style.display = "none"
        }
    });

   
}
function SubmitNickNameChange() {
    var nickName = $("input[name='nickName']").val()
    if (nickName == "") {
        layer.msg("用户名不能为空")
        return
    }
    if (!new RegExp("^[a-zA-Z0-9_\u4e00-\u9fa5\\s·]+$").test(nickName)) {
        layer.msg("用户名不能有特殊字符")
        return
    }
    else if (/(^\_)|(\__)|(\_+$)/.test(nickName)) {
        layer.msg('用户名首尾不能出现下划线')
        return
    }
    else if (/^\d+\d+\d$/.test(nickName)) {
        layer.msg('用户名不能全为数字')
        return
    }
    else {
        $.ajax({
            url: "/User/ChangeNickName",
            data: { "newName": nickName },
            type: "post",
            dataType: "json",
            error: function (xhr) {
                layer.msg('修改昵称失败')
            },
            success: function (data) {
                layer.open({
                    title: '结果'
                    , content: '修改昵称成功'
                    , yes: function (index, layero) {
                        window.location.href = "/User/PersonCenter"
                    }
                    , cancel: function () {
                        window.location.href = "/User/PersonCenter"
                    }
                });
            }
        })
    }
   
}
function ChangenEditPassword() {
    layer.open({
        title: "修改密码"
        , type: 1
        , content: $('#ChangePassword')
        , shade: 0.3
        , btnAlign: 'c' //按钮居中
        , move: false
        , cancel: function () {
            document.getElementById('ChangePassword').style.display = "none"
        }
    });

    layui.use('form', function () {
        var form = layui.form();
        form.verify({
           password: function (value, item) {
            if (!/^[\S]{6,16}$/.test(value)) {
                return '密码必须6到16位，且不能出现空格'
            }

        }
        , checkPassword: function (value, item) {
            if ($('#newPass').val() != value) {
                return '两次输入不一致'
            }

        }
        });
    });
}
function SubmitPasswordChange() {
    var pass = $("input[name='pass']").val()
    var newPass = $("input[name='newPass']").val()
    var chackNewPass = $("input[name='checkNewPass']").val()
    if (pass == "" || newPass==""||chackNewPass=="") {
        layer.msg("输入不能为空")
        return
    }
    if (!/^[\S]{6,16}$/.test(newPass)) {
        layer.msg("密码必须6到16位，且不能出现空格")
        return
    }
    else if (newPass != chackNewPass) {
        layer.msg('两次输入不一致')
        return
    }
    
    else {
        $.ajax({
            url: "/User/ChangePassword",
            data: { "password": pass,"newPassword":newPass },
            type: "post",
            dataType: "json",
            error: function (xhr) {
                layer.msg('修改密码失败,请确认密码输入正确')
            },
            success: function (data) {
                layer.open({
                    title: '结果'
                    , content: '修改密码成功'
                    , yes: function (index, layero) {
                        window.location.href = "/User/PersonCenter"
                    }
                    , cancel: function () {
                        window.location.href = "/User/PersonCenter"
                    }
                });
            }
        })
    }
}