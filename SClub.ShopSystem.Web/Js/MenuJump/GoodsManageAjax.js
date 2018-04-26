
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
        url: "/MenuJump/GoodsManage_ChangeTable",
        type: "GET",
        data: { "style": style },
        dataType: "Json",
        error: function (xhr) {
            layer.msg("没有该类型的订单")
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
                if(item.IfSale){
                    arr.push("<tr class=\"alter\">" +
                    "<td id=\"goodsId\" name=\"Id\" value=" +item.GoodsId + ">" + item.GoodsId + "</td>" +
                  "<td>" + item.GoodsName + "</td>" +
                  //  "<td style=\"\"><img src=\"" + goodsObj[i].GoodsImg + "\"></td>" +
                  "<td>" + item.GoodsCount + "</td>" +
                  "<td>" + item.SalesCount + "</td>" +
                  "<td>" + item.PurchasePrice + "</td>" +
                  "<td>" + item.Price + "</td>" +
                  "<td>" + item.SaleTime + "</td>" +
                  "<td><a href='/MenuJump/ChangeGoodsInfo?goodsId=" + item.GoodsId + "' >修改</a><br>" +
                  "<button type=\"submit\"class=\"layui-btn layui-btn-normal\" name=\"submit\" value=" + item.GoodsId + " >下架</button>" +
                  "<tr \>");
                }
                else{
                    arr.push("<tr class=\"alter\">" +
                   "<td id=\"goodsId\" name=\"Id\" value=" +item.GoodsId + ">" + item.GoodsId + "</td>" +
                 "<td>" + item.GoodsName + "</td>" +
                 //  "<td style=\"\"><img src=\"" + goodsObj[i].GoodsImg + "\"></td>" +
                 "<td>" + item.GoodsCount + "</td>" +
                 "<td>" + item.SalesCount + "</td>" +
                 "<td>" + item.PurchasePrice + "</td>" +
                 "<td>" + item.Price + "</td>" +
                 "<td>等待上架</td>" +
                 "<td><a href='/MenuJump/ChangeGoodsInfo?goodsId=" + item.GoodsId + "' >修改</a><br>" +
                 "<button type=\"submit\"class=\"layui-btn layui-btn-normal\" name=\"submit\" value=" + item.GoodsId + " >上架</button>" +
                 "<tr \>");
                }

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
