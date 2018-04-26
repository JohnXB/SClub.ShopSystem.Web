window.onload = function () {
    GoPage()
   
};

layui.use('element', 'layer', function () {
    var element = layui.element(); //导航的hover效果、二级菜单等功能，需要依赖element模块
    var layer = layui.layer;
});
function GoPage() {
   
    $.ajax({
        url: "/MenuJump/UserManage_GoPage",
        type: "GET",
        data: {},
        dataType: "Json",
        error: function (xhr) {
            layer.msg("获取用户失败")
        },
        success: function (data) {
            var adminObj = JSON.parse(data)
            AdminShowTable(adminObj)

        }

    })
}

function AdminShowTable(adminObj) {
    var adminArray = new Array(adminObj.length);
    for (var i = 0; i < adminArray.length; i++) {
        adminArray[i] = adminObj[i];
    }
    //将一段数组分页展示

    //测试数据
    layui.use(['laypage', 'layer'], function () {
        var laypage = layui.laypage
        , layer = layui.layer;

        var nums = 8; //每页出现的数据量

        //模拟渲染
        var render = function (data, curr) {
            var arr = []
            , thisData = data.concat().splice(curr * nums - nums, nums);
            layui.each(thisData, function (index, item) {
                for (var j = 0; j < item.AllRoles.length; j++) {
                    roleHTML = "";
                    roleHTML += (item.AllRoles[j].Name + " ");
                }
                arr.push("<tr class=\"alter\">" +
                    "<td name=\"Id\">" + item.UserId + "</td>" +
                    "<td>" + item.Name + "</td>" +
                    "<td >" + item.CreationTime + "</td>" +
                    "<td >" + roleHTML + "</td>" +
                    "<td ><button class=\"layui-btn layui-btn-normal\" value=" + item.UserId + " onclick='ResetPassword(value)'>重置密码</button></td>" +
                    '<td ><button class="layui-btn layui-btn-normal" onclick="PermissonManage('+ item.UserId+')">权限管理</button></td>' +
            "<tr \>");



            });
            return arr.join('');
        };
        //调用分页
        laypage({
            cont: 'paging'
          , pages: Math.ceil(adminArray.length / nums) //得到总页数
          , jump: function (obj) {
              document.getElementById('tableBody').innerHTML = render(adminArray, obj.curr);
          }
        });
    });

}
function PermissonManage(userId) {
    var zTreeObj;
    // zTree 的参数配置，深入使用请参考 API 文档（setting 配置详解）
    var setting = {
        check: {
            enable: true,
            chkStyle: "checkbox",
            chkboxType: { "Y": "ps", "N": "ps" }
        },
        data: {
            simpleData: {
                enable: true,
                idKey: "id",
                pIdKey: "pId",
                rootPId: 0
            }
        }
    };
    $.ajax({
        url: "/Backstage/GetAllRoles",
        type: "GET",
        data: { "userId": userId },
        dataType: "Json",
        error: function (xhr) {
            layer.msg("获取权限失败")
        },
        success: function (data) {
            document.getElementById("userIdInTree").value = userId
            var roleObj = JSON.parse(data)
            zTreeObj = $.fn.zTree.init($("#treeDemo"), setting, roleObj);
            layui.use( 'layer', function () {
                var layer = layui.layer;
            });
            layer.open({
                title: "权限管理 "
               , type: 1
               , content: $('#treeDem')
               , shade: 0.3
               , area: '300px'
               , btnAlign: 'c' //按钮居中
               , move: false
               , cancel: function () {
                   document.getElementById('treeDemo').innerHTML =""
                   document.getElementById('treeDem').style.display = "none"
               }
            });
        }

    })
}

//保存权限
function SavePermissionChange() {
    var userId = $("#userIdInTree").val();
    var treeObj = $.fn.zTree.getZTreeObj("treeDemo")
    nodes=treeObj.getCheckedNodes(true);
    var permissions = new Array()
    for (var i = 0; i < nodes.length; i++) {
        permissions[i] = nodes[i].name;
    }
    $.ajax({
        url: "/Backstage/PermissionManage",
        type: "GET",
        data: { "userId": userId, "permissions": permissions },
        dataType: "Json",
        traditional: true,
        error: function (xhr) {
            layer.msg("修改失败")
        },
        success: function (data) {
            if (data) {
                layer.open({
                    title: '结果'
                , content: '权限配置成功'
                    , yes: function (index, layero) {
                        window.location.href = "/MenuJump/UserManage"
                    }
                });
            }
            else {
                layer.msg('权限配置失败')
            }
        }
    })

}
function ResetPassword(UserId) {
    $.ajax({
        url: "/Backstage/ResetPassword",
        type: "GET",
        data: { "UserId": UserId },
        dataType: "Json",
        error: function (xhr) {
            layer.msg("重置失败")
        },
        success: function (data) {
            layer.msg("重置成功")

        }
    })
}