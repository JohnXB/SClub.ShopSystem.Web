function enableSubmit(bool) {

    if (bool) $("#submit").removeAttr("disabled");

    else $("#submit").attr("disabled", "disabled");

}

function v_submitbutton() {

    if ($("#agree").attr("checked") != "checked") {
       
        enableSubmit(false);

        $(".readagreement-wrap").css("outline", "1px solid #f99");

        return;

    }
    else {
        
        $(".readagreement-wrap").css("outline", "1px solid #9f9");
        
    }
for (f in flags) if (!flags[f]) { enableSubmit(false); return; }

        enableSubmit(true);
    

}

function showAgreement() {

    $("#readagreement").removeAttr("onclick");

    $("#agreement").show();

    $("#agreement iframe").attr("src", "");

}

function agree() {

    $("#agreement").hide();

    $("#readagreement").attr("onclick", "showAgreement();");

    if ($("#agree").attr("checked") != "checked") $("#agree").trigger("click");

}

function onReadAgreementClick() {
    if ($("#agree").attr("checked")) {
       
        $("#agree").removeAttr("checked");

    } else {
        $("#agree").attr("checked", "checked");

    }

    v_submitbutton();

}

var flags = [false, false, false, false];



var RegEmail = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;

function lineState(name, state, msg) {

    if (state == "none") { $("#line_" + name + " .input div").attr("class", "none"); return; }

    if (state == "corect") { $("#line_" + name + " .input div").attr("class", "corect"); return; }

    $("#line_" + name + " .input span").text(msg); $("#line_" + name + " .input div").attr("class", "error");

}

function v_account() {

    var account = $("#account").val();
    if (isNaN(account)) { lineState("account", "error", "只能由数字组成"); flags[0] = false; enableSubmit(false); }
    else if (account.length > 16) { lineState("account", "error", "超过长度"); flags[0] = false; enableSubmit(false); }
    else if (account.length < 6) { lineState("account", "error", "必须大于6位数"); flags[0] = false; enableSubmit(false); }
    else { lineState("account", "corect", ""); flags[0] = true; }

    v_submitbutton();

}

function v_name() {

    var name = $("#name").val();

    if (name.length == 0) { lineState("name", "error", "不得为空"); flags[1] = false; }

    else {

        if (name.length > 32) { lineState("name", "error", "必须少于32个字符"); flags[1] = false; }

        else { lineState("name", "corect", ""); flags[1] = true; }

    }

    v_submitbutton();

}

function v_password() {

    var password = $("#password").val();

    if (password.length < 6) { lineState("password", "error", "必须多于或等于6个字符"); flags[2] = false; }

    else {

        if (password.length > 16) { lineState("password", "error", "必须少于或等于16个字符"); flags[2] = false; }

        else {

            lineState("password", "corect", "");

            flags[2] = true;

        }

    }

    v_repeat();

    v_submitbutton();

}

function v_repeat() {

    if (!flags[2]) { lineState("repeat", "none", ""); return; }

    if ($("#password").val() != $("#repeat").val()) { lineState("repeat", "error", "密码不一致"); flags[3] = false; }

    else {

        lineState("repeat", "corect", "");

        flags[3] = true;

    }

    v_submitbutton();

}



function adaptValue() {

    return true;

}