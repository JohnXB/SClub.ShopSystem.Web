function enableSubmit(bool) {

    if (bool) $("#submit").removeAttr("disabled");

    else $("#submit").attr("disabled", "disabled");

}

function v_submitbutton() {

   { $(".readagreement-wrap").css("outline", "1px solid #9f9"); }

    for (f in flags) if (!flags[f]) { enableSubmit(false); return; }

    enableSubmit(true);

}





function onReadAgreementClick() {

    return;

    if ($("#agree").attr("checked")) {

        $("#agree").removeAttr("checked");

    } else {

        $("#agree").attr("checked", "checked");

    }

    v_submitbutton();

}

var flags = [false, false, false, false];




function lineState(name, state, msg) {
   
    if (state == "corect") {
        $("#" + name + "").hide();
       return;
    }

    if (state == "error") {
        $("#" + name).show();
        $("#" + name).text(msg);
        return;
    }
       
    
}

function v_goodsName() {

    var goodsName = $("#goodsName").val();

    
    if (goodsName.length == 0) { lineState("goodsNameTip", "error", "不得为空"); flags[0] = false; }

    else {

        if (goodsName.length > 32) { lineState("goodsNameTip", "error", "必须少于32个字符"); flags[0] = false; }

        else { lineState("goodsNameTip", "corect", ""); flags[0] = true; }

    }

    v_submitbutton();

}

function v_price() {

    var price = $("#price").val();
    if (price.length == 0) { lineState("priceTip", "error", "不得为空"); flags[1] = false; }
    else if (parseInt(price) < 0) {
        lineState("price", "error", "价格必须为正"); flags[1] = false;
    }
    else if (isNaN(price)) { lineState("priceTip", "error", "必须输入数字"); flags[1] = false; }
    else if (price.length > 10) { lineState("priceTip", "error", "必须少于10位数"); flags[1] = false; }
    else {
        lineState("priceTip", "corect", "");

        flags[1] = true;
    }
    

   
    v_submitbutton();

}

function v_purchasePrice() {
    var price = $("#price").val();
    var purchasePrice = $("#purchasePrice").val();
    if (purchasePrice.length == 0) { lineState("purchasePriceTip", "error", "不得为空"); flags[2] = false; }
    else if (isNaN(purchasePrice)) { lineState("purchasePriceTip", "error", "必须输入数字"); flags[2] = false; }
    else if (parseInt(purchasePrice) < 0) {
        lineState("purchasePriceTip", "error", "价格必须为正"); flags[2] = false;
    }
    else if (parseInt(purchasePrice) >= parseInt(price)) {
        lineState("purchasePriceTip", "error", "成本必须小于售价"); flags[2] = false;
    }
    
    else if (purchasePrice.length > 10) { lineState("purchasePriceTip", "error", "必须少于10位数"); flags[2] = false; }
    else {
        lineState("purchasePriceTip", "corect", "");

            flags[2] = true;
    }


    v_submitbutton();

}

function v_style() {
    
    var val = $('input:radio[name="goods[GoodsStyle]"]:checked').val();
        if(val==null){
            lineState("styleTip", "error", "请选择商品类型");
            return false;
            
        }
        
        else {
            return true;
        }


}

function v_intro() {
    var intro = $("#intro").val();
    if (intro.length == 0) { lineState("introTip", "error", "不得为空"); flags[3] = false; }
    else if (intro.length < 10) { lineState("introTip", "error", "必须大于10个字符"); flags[3] = false; }

    else {
        lineState("introTip", "corect", "");

        flags[3] = true;
    }


    v_submitbutton();

}

