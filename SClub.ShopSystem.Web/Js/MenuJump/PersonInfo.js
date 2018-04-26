function CheckPassword() {
    var newPassword = document.getElementById("changePassword").value;
    var checkNewPassword = document.getElementById("checkPassword").value;
    if (newPassword != checkNewPassword) {
        document.getElementById("tips").innerText = "两次密码不同";
        $("#submitPassword").attr("disabled", "disabled");
    }
    else {
        document.getElementById("tips").innerText = "";
        $("#submitPassword").removeAttr("disabled");
    }
}