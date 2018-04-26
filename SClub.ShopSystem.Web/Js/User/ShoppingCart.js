
layui.use('element', 'layer', function () {
    var element = layui.element(); //导航的hover效果、二级菜单等功能，需要依赖element模块
    var layer = layui.layer;
});
//单独结算
function BuyOneGoodsInCart(GoodsId) {
    document.getElementById(GoodsId).checked = true
    //取消其他选中状态
    var selectAll = document.getElementsByName("selectAll")
    var select = document.getElementsByName("cart[CartId]")
    var buyAll_btn = $("#BuyAll_Btn");
    selectAll[0].checked = false;
    for (i = 0; i < select.length; i++) {
        if (select[i].id != GoodsId) {
            select[i].checked = false
        }

    }
    document.getElementById("BuyAll_Btn").disabled = true;
    BuyGoodsInCart()
    Select()
}

function BuyGoodsInCart() {
    if (document.getElementById("BuyAll_Btn").disabled == false) {
        return;
    }
    $.ajax({
        url: "/User/GetAddressInCart",
        type: "GET",
        data: {},
        dataType: "Json",
        beforeSend: function () {
            layer.load(2); //
        },
        complete: function () {
            layer.closeAll('loading')
        },
        error: function (xhr) {
            CancelAll()
            layer.msg('获取收货地址失败')
        },
        success: function (data) {
            ShowAddress(data)
        }
    })

}
//确定结算按钮
function BuyGoodsInCartBtn() {
    var obj = document.getElementsByName("address");

    if ($('input:radio[name="address"]:checked').val() == null) {
        layer.msg("请选择收货地址");
        return;
    }
    else {
        OrderSave($('input:radio[name="address"]:checked').val())
    }

}
function OrderSave(addressId) {
    var count = 0
    var CartIdArray = []
    var select = document.getElementsByName("cart[CartId]")
    for (i = 0; i < select.length; i++) {
        if (select[i].checked == true) {
            CartIdArray[count] = select[i].value
            count++
        }
    }
    $.ajax({
        url: "/User/BuyGoodsInCart",
        type: "GET",
        data: { "cartIdArray": CartIdArray, "addressId": addressId },
        dataType: "Json",
        traditional: true,
        error: function (xhr) {
            layer.msg('生成订单失败')
        },
        success: function (data) {
            if (data) {
                layer.open({
                    title: '结果'
                , content: '生成订单成功'
                    , yes: function (index, layero) {
                        window.location.href = "/User/ShoppingCart"
                    }
                });
            }
            else {
                layer.msg('生成订单失败')
            }
        }
    })
}
//展示地址
function ShowAddress(data) {
    layui.use(['laypage', 'layer', 'form'], function () {
        var laypage = layui.laypage
        , layer = layui.layer;
        var form = layui.form()
        var addressList = JSON.parse(data)
        var nums = 8; //每页出现的数据量


        var addressArray = new Array(addressList.length);
        for (var i = 0; i < addressArray.length; i++) {
            addressArray[i] = addressList[i];
        }
        //模拟渲染
        var render = function (data, curr) {
            var arr = []
            , thisData = data.concat().splice(curr * nums - nums, nums);
            layui.each(thisData, function (index, item) {
                arr.push(
                     '<input type="radio" name="address" value="' + item.InfoId + '"  title="' + item.Address + item.Consignee + '收" >');
            });

            return arr.join('');
        };

        //调用分页
        laypage({
            cont: 'paging'
            , pages: Math.ceil(addressArray.length / nums) //得到总页数
            , jump: function (obj) {
                document.getElementById('address_List').innerHTML = render(addressArray, obj.curr);
            }
        });
        form.render('radio');
        layer.open({
            title: "选择地址"
           , type: 1
           , content: $('#address_Modal')
           , shade: 0.3
           , area: '500px'
           , btnAlign: 'c' //按钮居中
           , move: false
           , cancel: function () {

               document.getElementById('address_Modal').style.display = "none"
               CancelAll()

           }
        });
    });

}



function CancelAll() {
    var buyAll_btn = $("#BuyAll_Btn");
    var selectAll = document.getElementsByName("selectAll")
    var select = document.getElementsByName("cart[CartId]")
    selectAll[0].checked = false
    for (i = 0; i < select.length; i++) {
        select[i].checked = false
    }
    buyAll_btn.addClass("layui-btn-disabled")

}

function SelectAll() {
    var buyAll_btn = $("#BuyAll_Btn");
    var selectAll = document.getElementsByName("selectAll")
    var select = document.getElementsByName("cart[CartId]")

    if (selectAll[0].checked) {
        for (i = 0; i < select.length; i++) {
            select[i].checked = true
        }
        buyAll_btn.removeClass("layui-btn-disabled")
    }
    else {
        for (i = 0; i < select.length; i++) {
            select[i].checked = false
        }
        buyAll_btn.addClass("layui-btn-disabled")
    }
}
function Select() {
    var check = false;
    var selectAll = document.getElementsByName("selectAll")
    var select = document.getElementsByName("cart[CartId]")
    var buyAll_btn = $("#BuyAll_Btn");
    for (i = 0; i < select.length; i++) {
        if (select[i].checked == true) {
            buyAll_btn.removeClass("layui-btn-disabled")
            check = true;
        }
        else {
            selectAll[0].checked = false;
        }
    }
    if (!check) {
        buyAll_btn.addClass("layui-btn-disabled")
    }
}