window.onload = function () {
    ChangeTable("游戏")
};
layui.use('element', 'layer', function () {
    var element = layui.element(); //导航的hover效果、二级菜单等功能，需要依赖element模块
    var layer = layui.layer;
});
function ChangeTable(value) {
    
    var style = value;
    $.ajax({
        url: "/MenuJump/Inventory_ChangeTable",
        type: "GET",
        data: { "style": style },
        dataType: "Json",
        error: function (xhr) {
            layer.msg("没有该类型的商品")
        },
        success: function (data) {
            var goodsObj = JSON.parse(data)
            GoodsShowTable(goodsObj)

        }

    })
}

function GoodsShowTable(goodsObj) {
    var goodsArray = new Array(goodsObj.length);
    for (var i = 0; i < goodsArray.length; i++) {
        goodsArray[i] = goodsObj[i];
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
                
                arr.push("<tr class=\"alter\">" +
            "<td id=\"goodsId\" name=\"Id\" value=" + item.GoodsId + ">" + item.GoodsId + "</td>" +
                "<td id=\"goodsName\">" + item.GoodsName + "</td>" +
               // "<td style=\"\"><img src=\"" + goodsObj[i].GoodsImg + "\"></td>" +
                "<td id=\"count\">" + item.GoodsCount + "</td>" +
                "<td><button class=\"layui-btn layui-btn-normal\" value=" + item.GoodsId + " onclick=\"OpenModal1(value)\">入库</button>" +
                "<button class=\"layui-btn layui-btn-normal\" value=" + item.GoodsId + " onclick=\"OpenModal2(value," + item.GoodsCount + ")\">出库</button></td>" +
                "<tr \>");
                
                

            });
            return arr.join('');
        };
        //调用分页
        laypage({
            cont: 'paging'
          , pages: Math.ceil(goodsArray.length / nums) //得到总页数
          , jump: function (obj) {
              document.getElementById('tableBody').innerHTML = render(goodsArray, obj.curr);
          }
        });
    });

    
}

//模态框事件
function OpenModal1(goodsId) {
    var e1 = document.getElementById('modal-overlay-hill1');
    e1.style.visibility = (e1.style.visibility == "visible") ? "hidden" : "visible";
    document.getElementById("goodsIdAdd").value = goodsId;
}
function OpenModal2(goodsId, goodsCount) {
    var e1 = document.getElementById('modal-overlay-hill2');
    e1.style.visibility = (e1.style.visibility == "visible") ? "hidden" : "visible";
    document.getElementById("goodsIdSub").value = goodsId;
    document.getElementById("SubGoodsCount").value = goodsCount;
}
function ClouseModal1() {
    var e1 = document.getElementById('modal-overlay-hill1');
    e1.style.visibility = (e1.style.visibility == "visible") ? "hidden" : "visible";
}
function ClouseModal2() {
    var e1 = document.getElementById('modal-overlay-hill2');
    e1.style.visibility = (e1.style.visibility == "visible") ? "hidden" : "visible";
}

function CheckAddCount() {
   
    var nowCount = $("#AddInventory").val();
    if (nowCount.length == 0) {
        document.getElementById("addTip").innerText = "不得为空";
        $("#addSubmit").attr("disabled", "disabled");
        return;
    }
    else if (parseInt(nowCount) < 0) {
        document.getElementById("addTip").innerText = "请输入大于零的数";
        $("#addSubmit").attr("disabled", "disabled");
        return;
    }
    else if (isNaN(nowCount)) {
        document.getElementById("addTip").innerText = "必须输入数字";
        $("#addSubmit").attr("disabled", "disabled");
        return;
    }
    else if (nowCount.length > 10) {
        document.getElementById("addTip").innerText = "必须少于10位数";
        $("#addSubmit").attr("disabled", "disabled");
        return;
    }
    else {
        document.getElementById("addTip").innerText = "";
        $("#addSubmit").removeAttr("disabled");
    }



   

}
function CheckSubCount() {
    var nowCount = $("#SubInventory").val();
    var nowGoodsCount = $("#SubGoodsCount").val();
    if (nowCount.length == 0) {
        document.getElementById("subTip").innerText = "不得为空";
        $("#subSubmit").attr("disabled", "disabled");
        return;
    }
    else if (parseInt(nowCount) < 0) {
        document.getElementById("subTip").innerText = "请输入大于零的数";
        $("#subSubmit").attr("disabled", "disabled");
        return;
    }
    else if (isNaN(nowCount)) {
        document.getElementById("subTip").innerText = "必须输入数字";
        $("#subSubmit").attr("disabled", "disabled");
        return;
    }
    else if (nowCount.length > 10) {
        document.getElementById("subTip").innerText = "必须少于10位数";
        $("#subSubmit").attr("disabled", "disabled");
        return;
    }
    else if (parseInt(nowCount) > parseInt(nowGoodsCount)) {
        document.getElementById("subTip").innerText = "必须少于商品当前库存";
        $("#subSubmit").attr("disabled", "disabled");
        return;
    }
    else {
        document.getElementById("subTip").innerText = "";
        $("#subSubmit").removeAttr("disabled");
    }




}