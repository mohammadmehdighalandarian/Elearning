// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function unhidePassword() {
    var x = document.getElementById("password");
    if (x.type === "text") {
        x.type = "password";
    } else {
        x.type = "text";
    }
}

function checkPasswordStrength(password) {
    ;
    if (password.length <= 8) {
        return ".رمز باید حداقل 8 رقم باشد";
    }
    if (!password.match(/[a-z]/)) {
        return "رمز باید شامل حروف بزرگ و کوچک باشد.";
    }
    if (!password.match(/[A-Z]/)) {
        return "رمز باید شامل حروف بزرگ و کوچک باشد.";
    }
    if (!password.match(/[0-9]/)) {
        return "رمز باید شامل اعداد باشد.";
    }
    if (!password.match(/[^a-zA-Z0-9]/)) {
        return "رمز باید شامل % #  باشد.";
    }
    return "رمز انتخاب شده مناسب می باشد";
}

var passwordField = document.getElementById("password");
var strengthField = document.getElementById("strength");

passwordField.addEventListener("input", function () {
    var password = passwordField.value;
    var strength = checkPasswordStrength(password);
    strengthField.innerHTML = strength;
});
