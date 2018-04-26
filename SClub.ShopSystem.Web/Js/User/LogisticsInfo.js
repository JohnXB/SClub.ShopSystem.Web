layui.use('element', 'layer', function () {
    var element = layui.element(); //导航的hover效果、二级菜单等功能，需要依赖element模块
    var layer = layui.layer;
});
function GetReciptGoods() {
    layui.use(['laypage', 'layer'], function () {
        var laypage = layui.laypage
        , layer = layui.layer;
        $.ajax({
            url: "/User/ReciptGoodsPage",
            type: "POST",
            data: {},
            async: false,
            dataType: "Json",
            beforeSend: function () {
                layer.load(2)
            },
            complete: function () {
                layer.closeAll('loading')
            },
            error: function (xhr) {
                layer.msg("没有该类型的订单")
            },
            success: function (data) {
                if (data == false) {
                    layer.msg("没有该类型的订单")
                    return;
                }
                var orderList = JSON.parse(data);
                var orderArray = new Array(orderList.length);
                for (var i = 0; i < orderArray.length; i++) {
                    orderArray[i] = orderList[i];
                }
                //将一段数组分页展示

                //测试数据


                var nums = 8; //每页出现的数据量

                //模拟渲染
                var render = function (data, curr) {
                    var arr = []
                    , thisData = data.concat().splice(curr * nums - nums, nums);
                    layui.each(thisData, function (index, item) {
                        arr.push(
                                   '<tr class="alter" id="' + item.OrderId + '">' +
               '<td>' + item.OrderId + '</td>' +
                '<td>' + item.GoodsId + '</td>' +
                '<td>' + item.GoodsName + '</td>' +
                '<td>' + item.Price + '</td>' +
               ' <td>' + item.SalesCount + '</td>' +
                '<td>' + item.SalesCount * item.Price + '</td>' +
                '<td>' + item.DeliveryTime + '</td>' +
                '<td>' + item.Address + item.Consignee + ' (' + item.Tel + ')收</td>' +
                '<td><button class="layui-btn layui-btn-normal" onclick="ReciptGoodsButton(' + item.UserId + ',' + item.OrderId + ',' + item.GoodsId + ')">确认收货</button></td>' +
           ' </tr>');

                    });
                    return arr.join('');
                };
                //调用分页
                laypage({
                    cont: 'paging'
                  , pages: Math.ceil(orderArray.length / nums) //得到总页数
                  , jump: function (obj) {
                      document.getElementById('tableBody').innerHTML = render(orderArray, obj.curr);
                  }
                });



            }

        })
    });
}
//收货
function ReciptGoodsButton(userId, orderId, goodsId) {
    $.ajax({
        url: "/User/Recipt",
        type: "GET",
        data: { "userId": userId, "orderId": orderId, "goodsId": goodsId },
        dataType: "Json",
        error: function (xhr) {
            layer.msg('收货失败')
        },
        success: function (data) {
            if (data) {
                layer.open({
                    title: '结果'
                    , content: '收货成功'
                    , yes: function (index, layero) {
                        window.location.href = "/User/ReciptGoods"
                    }
                    , cancel: function () {
                        window.location.href = "/User/ReciptGoods"
                    }
                });
            }
            else {
                layer.msg('收货失败')
            }
        }
    })
}

function GetRecords() {
    layui.use(['laypage', 'layer'], function () {
        var laypage = layui.laypage
        , layer = layui.layer;
        $.ajax({
            url: "/User/RecordsPage",
            type: "POST",
            data: {},
            async: false,
            dataType: "Json",
            beforeSend: function () {
                layer.load(2)
            },
            complete: function () {
                layer.closeAll('loading')
            },
            error: function (xhr) {
                layer.msg("没有该类型的订单")
            },
            success: function (data) {
                if (data == false) {
                    layer.msg("没有该类型的订单")
                    return;
                }
                var orderList = JSON.parse(data);
                var orderArray = new Array(orderList.length);
                for (var i = 0; i < orderArray.length; i++) {
                    orderArray[i] = orderList[i];
                }
                //将一段数组分页展示

                //测试数据


                var nums = 8; //每页出现的数据量

                //模拟渲染
                var render = function (data, curr) {
                    var arr = []
                    , thisData = data.concat().splice(curr * nums - nums, nums);
                    layui.each(thisData, function (index, item) {
                        arr.push(
                        '<tr class="alter" id="' + item.OrderId + '">' +
                            '<td>' + item.OrderId + '</td>' +
                            '<td>' + item.GoodsId + '</td>' +
                            '<td>' + item.GoodsName + '</td>' +
                            '<td>' + item.Price + '</td>' +
                            '<td>' + item.SalesCount + '</td>' +
                            '<td>' + item.SalesCount * item.Price + '</td>' +
                            '<td>' + item.OrderTime + '</td>' +
                            '<td>' + item.DeliveryTime + '</td>' +
                            '<td>' + item.ReceiptGoodsTime + '</td>' +
                            '<td>' + item.Address + item.Consignee + '(' + item.Tel + ')收</td>' +
                     ' </tr>');

                    });
                    return arr.join('');
                };
                //调用分页
                laypage({
                    cont: 'paging'
                  , pages: Math.ceil(orderArray.length / nums) //得到总页数
                  , jump: function (obj) {
                      document.getElementById('tableBody').innerHTML = render(orderArray, obj.curr);
                  }
                });



            }

        })
    });
}
function GetDeliver() {
    layui.use(['laypage', 'layer'], function () {
        var laypage = layui.laypage
        , layer = layui.layer;
        $.ajax({
            url: "/User/DeliverPage",
            type: "POST",
            data: {},
            async: false,
            dataType: "Json",
            beforeSend: function () {
                layer.load(2)
            },
            complete: function () {
                layer.closeAll('loading')
            },
            error: function (xhr) {
                layer.msg("没有该类型的订单")
            },
            success: function (data) {
                if (data == false) {
                    layer.msg("没有该类型的订单")
                    return;
                }
                var orderList = JSON.parse(data);
                var orderArray = new Array(orderList.length);
                for (var i = 0; i < orderArray.length; i++) {
                    orderArray[i] = orderList[i];
                }
                //将一段数组分页展示

                //测试数据


                var nums = 8; //每页出现的数据量

                //模拟渲染
                var render = function (data, curr) {
                    var arr = []
                    , thisData = data.concat().splice(curr * nums - nums, nums);
                    layui.each(thisData, function (index, item) {
                        arr.push(
                            '<tr class="alter">' +
                            '<td>' + item.OrderId + '</td>' +
                            '<td>' + item.GoodsName + '</td>' +
                            '<td>' + item.Price + '</td>' +
                            '<td>' + item.SalesCount + '</td>' +
                            '<td>' + item.SalesCount * item.Price + '</td>' +
                            '<td>' + item.OrderTime + '</td>' +
                            '<td>' + item.Address + item.Consignee + ' (' + item.Tel + ')收</td>' +
                            '<td>待发货</td>' +
                        '</tr>');

                    });
                    return arr.join('');
                };
                //调用分页
                laypage({
                    cont: 'paging'
                  , pages: Math.ceil(orderArray.length / nums) //得到总页数
                  , jump: function (obj) {
                      document.getElementById('tableBody').innerHTML = render(orderArray, obj.curr);
                  }
                });



            }

        })
    });
}