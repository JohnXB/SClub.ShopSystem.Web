
window.onload = function () {
    GoPage()
};
layui.use('element', 'layer', function () {
    var element = layui.element(); //导航的hover效果、二级菜单等功能，需要依赖element模块
    var layer = layui.layer;
});
function GoPage() {
    $.ajax({
        url: "/MenuJump/Record_GoPage",
        type: "GET",
        data: { },
        dataType: "Json",
        error: function (xhr) {
            layer.msg("没有该类型的商品")
        },
        success: function (data) {
            var recordObj = JSON.parse(data)
            RecordShowTable(recordObj)

        }

    })
}

function RecordShowTable(recordObj) {
    var recordArray = new Array(recordObj.length);
    for (var i = 0; i < recordArray.length; i++) {
        recordArray[i] = recordObj[i];
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
                    "<td name=\"Id\">" + item.RecordId + "</td>" +
                    "<td>" + item.UserId + "</td>" +
                    "<td >" + item.RecordName + "</td>" +
                    "<td >" + item.RecordTime + "</td>" +
                    "<td >" + item.GoodsId + "</td>" +
                    "<td >" +item.OrderId + "</td>" +
            "<tr \>");



            });
            return arr.join('');
        };
        //调用分页
        laypage({
            cont: 'paging'
          , pages: Math.ceil(recordArray.length / nums) //得到总页数
          , jump: function (obj) {
              document.getElementById('tableBody').innerHTML = render(recordArray, obj.curr);
          }
        });
    });
}