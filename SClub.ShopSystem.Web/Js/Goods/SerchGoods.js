window.onload = function () {
    var serchItem = $("#style").text();
    SerchGoods(serchItem)
};

function SerchGoods(serchItem) {
    if (serchItem == "") {
        layui.use('layer', function () {
            var layer = layui.layer;
            layer.msg("搜索内容不能为空！")
        })
        return;
    }
    layui.use(['laypage', 'layer'], function () {
        var laypage = layui.laypage
        , layer = layui.layer;
        $.ajax({
            url: "/Goods/SerchItem",
            type: "POST",
            data: { "serchItem": serchItem },
            async: false,
            dataType: "Json",
            beforeSend: function () {
                layer.load(2)
            },
            complete: function () {
                layer.closeAll('loading')
            },
            error: function (xhr) {
                document.getElementById('goods_List').innerHTML = '<span class="notFount">没有该类型的商品</span>'
            },
            success: function (data) {
                if (data == false) {
                    document.getElementById('goods_List').innerHTML = '<span class="notFount">没有该类型的商品</span>';
                    return;
                }
                
                var goodsList = JSON.parse(data);
                if (goodsList.length == 0) {
                    document.getElementById('goods_List').innerHTML = '<span class="notFount">没有该类型的商品</span>';
                    return;
                }
                var goodsArray = new Array(goodsList.length);
                for (var i = 0; i < goodsArray.length; i++) {
                    goodsArray[i] = goodsList[i];
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
                            '<div class="goods-box">' +
                                '<div class="goods-img">' +
                                    '<a href="/Goods/GoodsDetails?id=' + item.GoodsId + '">' +
                                        '<img src="' + item.Img + '" />' +
                                    '</a>' +
                                '</div>' +
                                '<div class="goods-detail">' +
                                    '<div class="goods-price"><span class="goods-Price">￥' + item.Price + '</span>  <span class="goods-Sale">' + item.SalesCount + ' 人付款</span></div>' +
                                '</div>' +
                                '<div class="goods-name">' + item.GoodsName + ' </div>' +
                                '<div>' +
                                    '<input type="hidden" name="no" value="' + item.GoodsId + '" />' +
                               ' </div>' +
                            '</div>');

                    });
                    return arr.join('');
                };
                //调用分页
                laypage({
                    cont: 'paging'
                  , pages: Math.ceil(goodsArray.length / nums) //得到总页数
                  , jump: function (obj) {
                      document.getElementById('goods_List').innerHTML = render(goodsArray, obj.curr);
                  }
                });



            }

        })
    });

}
function ShowList(style) {
    layui.use(['laypage', 'layer'], function () {
        var laypage = layui.laypage
        , layer = layui.layer;
        $.ajax({
            url: "/Goods/ShowGoodsList",
            type: "POST",
            data: { "style": style },
            async: false,
            dataType: "Json",
            beforeSend: function () {
                layer.load(2)
            },
            complete: function () {
                layer.closeAll('loading')
            },
            error: function (xhr) {
                document.getElementById('goods_List').innerHTML = '<span>没有该类型的商品</span>'
            },
            success: function (data) {
                if (data == false) {
                    document.getElementById('goods_List').innerHTML = '<span>没有该类型的商品</span>';
                    return;
                }
                var goodsList = JSON.parse(data);
                var goodsArray = new Array(goodsList.length);
                for (var i = 0; i < goodsArray.length; i++) {
                    goodsArray[i] = goodsList[i];
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
                            '<div class="goods-box">' +
                                '<div class="goods-img">' +
                                    '<a href="/Goods/GoodsDetails?id=' + item.GoodsId + '">' +
                                        '<img src="' + item.Img + '" />' +
                                    '</a>' +
                                '</div>' +
                                '<div class="goods-detail">' +
                                    '<div class="goods-price"><span class="goods-Price">￥' + item.Price + '</span>  <span class="goods-Sale">' + item.SalesCount + ' 人付款</span></div>' +
                                '</div>' +
                                '<div class="goods-name">' + item.GoodsName + ' </div>' +
                                '<div>' +
                                    '<input type="hidden" name="no" value="' + item.GoodsId + '" />' +
                               ' </div>' +
                            '</div>');

                    });
                    return arr.join('');
                };
                //调用分页
                laypage({
                    cont: 'paging'
                  , pages: Math.ceil(goodsArray.length / nums) //得到总页数
                  , jump: function (obj) {
                      document.getElementById('goods_List').innerHTML = render(goodsArray, obj.curr);
                  }
                });



            }

        })
    });

}