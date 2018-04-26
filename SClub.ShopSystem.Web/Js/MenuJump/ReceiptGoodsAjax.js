window.onload = function () {
    ChangeTable("游戏")
};
layui.use('element', 'layer', function () {
    var element = layui.element();
    var layer = layui.layer;
});
function ChangeTable(value) {
    var style = value;
    $.ajax({
        url: "/MenuJump/Order_Receipt",
        type: "GET",
        data: { "style": style },
        dataType: "Json",
        error: function (xhr) {
             layer.msg("没有该类型的订单")
        },
        success: function (data) {
            var orderObj = JSON.parse(data)
            OrdersShowTable(orderObj)

        }

    })
}



function OrdersShowTable(orderObj) {

    var orderArray = new Array(orderObj.length);
    for (var i = 0; i < orderArray.length; i++) {
        orderArray[i] = orderObj[i];
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
                arr.push( '<tr class="alter">' +
                    '<td name="Id" value=' + item.OrderId + '>' + item.OrderId + '</td>' +
                    '<td>' + item.GoodsName + '</td>' +
                    '<td >' + item.SalesCount + '</td>' +
                    '<td >' +item.Price + '</td>' +
                    '<td >' + item.DeliveryTime +'</td>' +
                    '<td >' +item.UserId +'</td>' +
                    '<td >待收货</td>' +
                     '<tr \>');

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
    });
}


